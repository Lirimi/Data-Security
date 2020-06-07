# ds
* Projekti ne lenden "Siguria e te dhenave". Realizimi i funksioneve u implmentua ne gjuhen programuese C#


1. Ekzekutimi i Programit: 

* Supozojme se jemi ne pathin e duhur atehere:

* Hapi 1: Kompailimi ne Terminal behet duke përdorur komandën  "msbuild ds.sln" per te ndertuar projektin ne pathin Github/ds ku ekziston edhe .sln file ose debug nga VS Code
* Hapi 2: Ekzekutimi dhe thirrja e main behet ne pathin ku u krijua ds.dll zakonisht ne shtegun ds/ds/bin/debug/netcoreapp3.1 duke perdorum komanden vetem ds.exe(ds) per platformen Windows ndersa dotnet ds.dll per platformen Linux ose Mac pastaj therrasim argumentet si me poshte: =>

	1.Ekzekutimi i funksionit Beale:
		ds Beale Encrypt  <text>
     	ds Beale Decrypt  <text>

	2.Ekzekutimi i funksionit Permutation:
		ds Permutation Encrypt  <key><text>
     	ds Permutation Decrypt  <key><text>

	3.Ekzekutimi i funksionit Numerical:
		ds Numerical Encode  <text>
     	ds Numerical Decode  <code>

    4.Ekzekutimi i komandes create-user:
		ds create-user  <name>
     	
	5.Ekzekutimi i komandes delete-user:
		ds delete-user  <name>
     	
	6.Ekzekutimi i komandes export-key:
		ds export-key <public|private> <name> [file]
    
	7.Ekzekutimi i komandes import-key:
		ds import-key  <name> <path>
     	
    8.Ekzekutimi i komandes write-message:
		ds write-message  <name> <message> [token|file] [file] 
    
	9.Ekzekutimi i komandes read-message:
		ds read-message  <encrypted-message>

   10.Ekzekutimi i komandes login:
   		ds login <name>

   11.Ekzekutimi i komandes status:
   		ds status <token>

2. Pershkrimi i Komandave: 

FAZA I:

* Komanda Beale: 	Zevendson secilin shkronje te  Plaintextit me poziten e saj ne nje liber.

*  Komanda Permutation: Transformon Plaintextin(i cili ndahet ne blloqe) ne baze te nje permutacioni me ane te qelsit te dhene. Gjatesia e qelesit duhet domosdo te jete sa gjatesia e bloqeve.
Qelsi duhet te permbaje numra jo te persitur. Ne algoritmin tone Plaintext eshte i ndare ne blloqe nga 4 keshtu qe edhe qelsi mundet te jete vetem si permutacion i numrave 1234, ku secili numer i pergjigjet pozites se plaintextit ne bllok.

* Komanda Numerical:    Secila shkronjë zëvendësohet me pozitën e saj në alfabetin e gjuhes angleze. Kështu shkronja “a” enkodohet në numrin 1, shkronja “b” në numrin 2 e keshtu me radhe.

FAZA II:

* Komanda create-user: Krijon nje pale RSA key te direktoriumi keys(path relativ) qe permban qelsin privat dhe publik te nje perdoruesi.

* Komanda delete-user: Fshine palen RSA key nga direktoriumi keys(path relativ) qe permban qelsin publik dhe privat. Ka raste kur komanda do te fshije vetem qelsin publik nese qelsi privat nuk ekziston ne direktorium.

* Komanda export-key: Exporton RSA qelsin publik ose privat te shfrytezuesit nga direktoriumi i celesave. [file] eshte nje argument opsional qe ne rast se jepet mund te jete si path relativ ose absolut ku do te ruhet qelsi i eksportuar. Nese ky argument nuk jepet ose mungon atehere RSA key do te shfaqet ne console(terminal).

* Komanda import-key: Importon RSA qelsin publik ose privat te shfrytezuesit nga nje path qe mund te jete relativ ose absolut(ndaj vendit ku behet ekzekutimi i programit) dhe e vendos ne direktorium e qelesave(keys). Ne rast se ne instancen RSA detektohet qe ka permbajtje te qelsit privat atehere automatikisht importohet edhe qelsi publik ne nje file te vecante ku gjithashtu ruhet ne dir keys. Nese argumenti <path> fillon me http:// ose https:// atehere do te dergohet nje GET request te URL pathi qe te marr trupin e pergjiegjes si qels valid. Instanca RSA me pas mundeson ta dij nese trupi permban qels valid RSA ose se a eshte qels publik ose privat.  

* Komanda write-message: Enkripton nje mesazh te dedikuar per nje shfrytezues. Mesazhi i enkriptuar do te ndahet ne 4 komponente me karakterin "." si ndares. Komponenti i pare paraqet enkodimin e Emrit dedikuar shfrytezuesit ose marresit, kompanenta e dyte vektorin e inicilizuar IV, komponenti i trete paraqet DES qelsin te enkriptuar me RSA, dhe komponenta e fundit paraqet mesazhin e enkriptuar me DES. Mesazhi i enkriptuar mund te shfaqet ne Console ose te ruhet ne  nje path te relativ/absolut.

* Komanda read-message: Duke perdorur qelsin RSA privat te marresit Dekriptohet mesazhi i enkriptuar dhe shfaqet ne console.


FAZA III:

• Pershkimi i komandave:
	* Komanda create-user:   Zgjerohet komanda create-user e fazes se dyte ashtu qe gjate krijimit te shfrytezuesit te kerkohet edhe fjalekalimi dhe te dhenat te ruhen ne baze te shenimeve.

	* Komanda delete-user:   Zgjerohet komanda delete-user e fazes se dyte ashtu qe gjate fshirjes se qelsave do të fshihen edhe të gjitha të dhënat e shfrytëzuesit nga baza e shenimeve.

	* Komanda login: 	     Teston çiftin shfrytëzues/fjalëkalim. Në rast suksesi lëshohet një token i nënshkruar i cili mund të përdoret për autentikim të shfrytëzuesit.

	* Komanda status:        Jep informata rreth tokenit. Nese tokeni ka skaduar apo nuk ka nenshkrim valid atehere tokeni nuk eshte valid.

	* Komanda write-message: Kjo komandë zgjerohet ashtu që mund ta pranojë edhe opsionin --sender <token>. N.q.s se tokeni nuk eshte valid atehere komanda deshton, ndersa nese tokeni validohet me sukses atehere behet nenshkrimi i mesazhit te enkriptuar me qels privat te derguesit(Vlere qe nxirret nga tokeni!). Ne rast se opsioni sender nuk specifikohet atehere veprohet sikurse ne fazen e dyte.

	* Komanda read-message:  Komanda read-message zgjerohet ashtu që nëse figuron pjesa e dërguesit/nënshkrimit në mesazh, atëherë do të tentohet verifikimi i atij nënshkrimi duke përdorur çelësin publik të dërguesit. Nëse mungon pjesa e dërguesit/nënshkrimit, atëherë komanda e injoron dhe vepron sikur në fazën e dytë.

• Detajet e implementimit për:
	** Skemën e ruajtjes së fjalëkalimeve - Fjalekalimet jane te ruajtuara jo ne forme te plaintext por ne forme hashing+salt. Salt gjenerohet e re(random) per cdo shfrytezues dhe kombinohet me hashing algorithm SHA-256 per nje forme me te sigurt te ruajtjes se fjalekalimeve. Te pjesa e login implementohet algoritmi i njejte vecse tash salt merret nga baza e te dhenave per shfrytezuesin ne fjale. Ne rast se hashed password i shfrytezuesit eshte i njejte me ate te qe gjendet ne baze te dhenave atehere per shfrytezuesin gjenerohet nje token.

	** Mënyrën e ruajtjes së shënimeve -  Shenimet jane te ruajtuara ne nje databaze. Ne databaze ruhen te dhenat kryesore per nje shfrytezues, sic jane emri i perdoruesit, passwordi i ruajtur ne forme te enkriptuar(hashing + salt) dhe gjithashtu ruhet edhe salt e gjeneruar per shfrytezuesin pasi qe na duhet per verifikimin korrekt te password te pjesa e Login. 

	** Strukturën e tokenëve të lëshuar - Tokeni permbahet nga tri komponente. Kompanenti i pare parqet algoritmin dhe tipin e tokenit(Ne rastin tone JWT). Kompenti i dyte ne permbajtje ka emrin e shfrytezuesit(sub) dhe kohen e skadimit(exp) ne unix datetime ndersa komponenti i trete paraqet nenshkrimin e tokenit me qelsin privat te shfrytezuesit.

      
3. Per te pare Rezultatet e ekzekutimit referohu tek '(FAZA 1)Raporti i projektit' perkatesisht '(FAZA 2)Rezultatet e ekzekutimeve' dhe (FAZA 3)Rezultatet e ekzekutimeve!









