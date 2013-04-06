#Beskrivelse av demoer for RavenDB-meetup

##Besøk fra demospøkelset

Alle som var tilstede på meetup'en så at demospøkelset var skikkelig tilstede. Noen av demoene 
funket litt etterhvert, men det ble ikke helt 100% uansett.

I ettertid har jeg funnet ut hvorfor dette skjedde. Når man starter prosjektet i IIS Express
vil det ikke initialiseres fra bunnen av dersom man ikke har gjort endringer i koden siden
forrige session. Derfor måtte jeg stoppe applikasjonen i IIS Express for å få denne til å
kjøre normale startup-tasks som å generere indekser o.l. Resten av det som skjedde var i 
praksis følgefeil.

For at de som måtte være interessert skal få en sjanse til å følge demoene, beskriver jeg 
dem her slik jeg hadde tenkt til å gjennomføre dem.

###Demo - RavenDB fra scratch

Denne ble forsåvidt gjennomført ok - slik kan man installere RavenDB for test eller på en
utviklermaskin.

1. Last ned zip-fil fra ravendb.net med siste versjon av serverdistribusjonen.
2. Lag en katalog et sted på lokalt filsystem for RavenDB.
3. Pakk ut zip-fila i denne katalogen.
4. Gå inn i Server-katalogen
5. Start Raven.Server.exe
6. Gå inn på [http://localhost:8080/](http://localhost:8080/) - dette er Raven Studio.
7. Opprett en testdatabase når dialogboksen spretter opp.
8. Observér at REST-requester til serveren dukker opp i debug-vinduet.

*Merk*: RavenDB velger port 8080 som default. Dersom den er opptatt, velger den 8081 osv.
Dersom du ønsker å styre dette selv, kan du åpne Raven.Server.exe.config og endre appsettingen
Raven/Port til ønsket port.

###Demo - RavenDB som en service

Dette er én vanlig måte å kjøre RavenDB i produksjon.

1. Steng RavenDB fra forrige demo.
2. Åpne en command prompt og gå til Server-katalogen til RavenDB.
3. Kjør

	Raven.Server.exe /install

4. Åpne "Services" på maskinen og verifiser at RavenDB er installert og kjører.
5. Gå inn på [http://localhost:8080/](http://localhost:8080/) og verifiser at den funker.
6. Stopp tjenesten og avinstallér den:

	Raven.Server.exe /uninstall

###Demo - Seed av databasen

MVC-applikasjonen har en metode for å generere litt halv-random testdata.

*OBS*: Det var her demospøkelset dukket opp. For ordens skyld, restart IIS Express dersom 
applikasjonen skulle ha vært kjørt allerede.

1. Start Raven.Server.exe som applikasjon igjen.
2. Sjekk web.config og RavenDB-connectionstring som peker på localhost og MeetDown-databasen.
3. Gå inn i Raven Studio og verifisér at det ikke finnes noen MeetDown-database.
4. Kjør applikasjonen - det er ingen meetdowns i lista man kommer til.
5. Gå til URLen /seed på web-app'en. Dette genererer random data og legger inn i databasen.
6. Dersom lista fortsatt er tom, skyldes dette *stale index* - vises frem i neste demo. 
Trykk F5 så burde de dukke opp.
7. Trykk deg rundt på meetdowns, brukere og tags for å se hvordan ting henger sammen.
8. Sjekk Raven Studio og se at dokumentene er i databasen.

###Demo - Stale indexes

Hvis man ikke spesifiserer at man vil vente, kan RavenDB gi tilbake "stale" data. Som
det ble kommentert under meetup'en, vil dette i praksis være det samme som Dirty Reads.

1. Gå til Raven Studio, slett alt innhold i MeetDown-databasen.
2. Gå til forsiden på webapp - merk tom meetdown-liste.
3. Kjør /seed-action igjen.
4. Gjenta disse punktene til det ikke kommer opp grupper i lista.

Kjører man på en for rask maskin kan det hende man aldri opplever dette problemer - men det
er likevel viktig å være klar over fenomenet dersom man bruker RavenDB i en produksjonsapp.

###Demo - Unit testing med RavenDB

Istedenfor å mocke en IDocumentSession i unit tests, hvorfor ikke bare kjøre mot en
embedded utgave av databasen som kjører i minnet?

1. Åpne MeetDown.Tests\TestBase\RavenDBTest.cs - dette er min simple base class for tester
som skal teste kode som rører RavenDB.
2. Merk at EmbeddableDocumentStore er en variant av RavenDB som kan kjøre som en del av
din applikasjon. Parameteret RunInMemory sørger for at data ikke rører disken.
3. Sjekk ut NoStaleQueriesListener. Denne setter et parameter for å sørge for at vi alltid
venter på index-oppdatering før vi får tilbake data. Dette kan også gjøres i prod dersom man
er obs på konsekvensene, og det bør nevnes at man bør forstå hva dette innebærer dersom man
kjører det på test og ikke i prod, som her.
4. Åpne MeetDown.Tests\Web\Controllers\UserControllerTests.cs og sjekk ut testmetoden 
Info _ WhenUserExists _ ReturnsExpectedViewAndCorrectModel() (litt ekstra space her pga.
MarkDown).
5. Se hvordan metoden åpner sessions, automatisk sørger for å dispose dem, og verifiserer
at vi får ut rett data.

###Demo - MVC-routing-problem

Ikke så mye et demo som en kommentar til måten MVC håndterer routing på og at dette gjerne
krasjer med RavenDB sin tendens til å kalle dokumenter f.eks. users/1. 

I denne applikasjonen er dette løst i RavenDbModule ved å sette IdentityPartsSeparator når
DocumentStore blir opprettet.

###Demo - Include og hva den brukes til

Dette viser at man ikke er helt lost selv om man ikke har relasjoner til andre tabeller.

1. Åpne GroupController, se på Info-actionmetoden.
2. Åpne GroupInfoModel som brukes til å lage Info-sider om grupper.
3. Sjekk et group-dokument i Raven Studio og se at den har en liste med referanser til 
medlems-Id'er.
4. Åpne RavenDB.Server.exe-vinduet og skriv "cls" og trykk enter. Dette renser skjermen.
5. Gå inn på siden til en gruppe i web-app'en.
6. Se i debug-vinduet til RavenDB-server - kun én request ble sendt til server, selv om vi 
hentet mange dokumenter - gruppa og alle medlemmene.

###Demo - RavenProfiler

Dette kan brukes hvis man ønsker å sjekke ytelse på database-bruken i en webapplikasjon.

1. Installér NuGet-pakka Raven.Client.MvcIntegration (dette er allerede gjort i min applikasjon).
2. Gå til MeetDown.Core\Modules\RavenDbModule.cs og se at den initialiserer profiler.
3. Gå til MeetDown.Web\Views\Shared\_Layout.cshtml og fjern kommenteringen av 
RavenProfiler-kallet.
4. Kjør app, sjekk RavenProfiler-output i øvre venstre hjørne. Dette er ganske likt
MiniProfiler for de som kjenner det.
5. Gå inn på info-siden til en gruppe og se på JSON-resultatet vi fikk fra databasen -
hvordan denne ser ut når vi fikk mange dokumenter.

###Demo - Map/Reduce index m. TagsByPopularity

Dette er et enkelt eksempel på en Map/Reduce index.

1. Åpne et group-dokument i Raven Studio.
2. Observér at tags kun er en collection med strings pr. gruppe - ingen egen entity.
3. Gå til /tags/ i webapp og se statistikken. Trykk på en tag, se på gruppe-lista den er med i.
4. Hvordan kan vi enkelt få frem denne typen informasjon? Map/Reduce lar oss prekalkulere
dette, slik at informasjonen er lynraskt tilgjengelig når vi ønsker å spørre etter den.
5. Åpne MeetDown.Core\Indexes\TagsByPopularity.cs.
6. Map - delen av indexet henter ut hver tag fra hver gruppe, og tildeler den et antall på 1.
7. Reduce - delen får resultatet av Map, grupperer på tag-navnet, og summerer antallet. Dermed
har vi oversikt over hvor mange steder hver tag dukker opp.
8. Dersom data senere endres (nye grupper, nye tags, tags fjernes) vil index fores med nye
verdier, og reduce kan effektivt summere opp det som er endret.
9. Åpne MeetDown.Web\Controllers\TagsController.cs og se hvordan man utfører en spørring
mot indekset.

###Demo - Replication

Dette er bare et kjapt eksempel på enveis-replikering av data mellom databaser.

1. Slett MeetDown-databasen på hovedserver.
2. Lag en ny katalog for en ny RavenDB-server, pakk ut innhold av zip-fil dit. Start server.
3. Lag MeetDown-databasen på nytt på hovedserver. Kryss av for Replication bundle - disse
kan man ikke legge til senere. I instillingene for databasen, legg til replication, og
spesifiser at replisering skal skje mot http://localhost:8081/ (eller hva sekundær-server
enn blir kjørende som) og databasen MeetDown.
4. Lag MeetDown-database på server nr. 2, og sett på replication her også. Legg til replication
settings, men ikke sett inn noe config.
5. Kjør seed på web-app.
6. Kjør Raven Studio mot begge servere og verifiser at dokumentene er begge steder.
7. Fortsett direkte videre til failover-demo.

Man kan også lett kjøre toveis-replisering ved å få server 2 til å peke på server 1. Det
er ingen begrensninger i forhold til hvor mange servere man kan involvere.

###Demo - Failover

Dette viser hvordan RavenDB-klienten håndterer at serveren dør. Den har allerede ved oppstart
hentet ut replication-info fra "sin" server, og vil dermed forsøke andre servere når den
sliter med å få svar på requests.

1. Stopp server nr. 1.
2. Trykk på friske linker i web-app.
3. Legg merke til at ting tar litt tid, men at requests fortsatt blir oppfylt - fra server 2.
4. Eneste config vi har på klienten er fortsatt connection string i web.config.

Dett var dett.
