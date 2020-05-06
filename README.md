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
		ds write-message  <name> <message> [file]
    
	9.Ekzekutimi i komandes read-message:
		ds read-message  <encrypted-message>

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
      
3. Per te pare Rezultatet e ekzekutimit referohu tek 'Raporti i projektit'(Faza 1) perkatesisht 'Rezultatet e ekzekutimeve'(Faza 2)!









