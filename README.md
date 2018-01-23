# HCI
Projekat iz HCI-ja br 6

----VOĐENJE EVIDENCIJE O MAPI MANIFESTACIJA U NEKOM GRADU

Napraviti jednostavnu aplikaciju za vođenje evidencije o geografskoj distribuciji manifestacija
u nekom gradu. Potrebno je realizovati distribuciju preko mape grada na koju se prevlače i spuštaju
simboli različitih manifestacija. Mapa je fiksna slika koja se ne skroluje i ne zumira, i neophodno je da
je studenti nađu sami. Svi podaci se čuvaju u fajlu i učitavaju prilikom startovanja aplikacije.
Svaka manifestacija je opisana preko: svoje jedinstvene ljudski-čitljive oznake koju unosi
korisnik, imena, opisa, tipa, statusa za služenje alkohola, ikonice, da li je dostupna za hendikepirane,
da li je na njoj dozvoljeno pušenje, da li je napolju ili unutra, kategorije cena, očekivane publike, i
datuma održavanja. Ikonica je sličica koja se učitava i koja se koristi da se manifestacija označi na mapi
i može da se i ne postavi i, ako se ne postavi, onda se podrazumevano uzima ikonica tipa. Status
služenja alkohola je jedna od sledećih vrednosti: nema alkohola, alkohol se može doneti, alkohol se
može kupiti na licu mesta. Kategorija cena je jedna od sledećih vrednosti: besplatno, niske cene,
srednje cene, visoke cene. Manifestacije takođe mogu biti i "tagovane" sa nijednom, jednom, ili više
etiketa. Etikete specificira korisnik i one su opisane svojom jedinstvenom ljudski-čitljivom oznakom
koju unosi korisnik, bojom i opisom.
Tip manifestacije je opisan preko svoje jedinstvene ljudski-čitljive oznake koju unosi korisnik,
imena, ikonice, i opisa. Ikonica je sličica koja se učitava i koja se koristi da se taj tip manifestacije
označi na mapi.


Osnovni zadaci aplikacije. Aplikacija treba da obezbedi:
1. Ažuriranje osnovnih podataka o manifestacijama, tipovima i etiketama.
2. Prikaz mape i direktnu manipulaciju simbola na mapi na pregledan način.
3. Nameštanje tipa manifestacijama kao i njihovo "tagovanje" etiketama.
4. Tabelarni pregled manifestacija uz filtriranje i pretragu.
5. Sistem pomoći.


Zadatak obavezno realizovati direktnom manipulacijom i upotrebom drag&drop
tehnike. Omogućiti korisniku:
 Vizuelni pregled mape.
 Vizuelni pregled rasporeda manifestacija.
 Prevlačenje predstave manifestacije sa kontrole koja vizuelizuje skup dostupnih manifestacija
na predstavu mape. Manifestacije se ne smeju preklapati.
 Prikaz svih atributa bitnih za jasnu i jedinstvenu identifikaciju manifestacije prilikom izbora. 

----DODATNA FUNKCIONALNOST 1—LOGOVANJE

Implementirati u okviru aplikacije podsistem koji omogućava prijavljivanje korisnika za rad u
aplikaciji koristeći nekakav mehanizam potvrđivanja identiteta (lozinka i sl.). Fokus ovde apsolutno
nije na samoj funkcionalnosti—koja treba da bude implementirana apsolutno minimalno bez straha
od gubitka bodova—već na interfejsu koji treba da je podrži. 

----SCENARIO KORIŠĆENJA W
U ovom scenariju, program se koristi za unos izuzetno velike količine podataka. Zbog toga,
brzina, neposrednost pristupa funkcijama unosa, izmene i brisanja, i efikasnost prečica su od
maksimalne važnosti. 


----KORISNIK B
Pol: Muški.
Starost: 22 godina
Domensko znanje:  Slabo. Korisnik nije stručan u oblasti u kojoj radi aplikacija.
Znanje rada na računaru: Slabo. Korisnik nema puno iskustva u radu sa računarom.
Ograničavajuće osobine: Korisnik je daltonista, i uopšte ne razlikuje boje. Takođe, kao što je se iz
pređašnjeg može zaključiti, nema nikakvu obuku za okruženje u kome radi i
oslanja se u velikoj meri na online sisteme pomoći.

