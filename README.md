# ds
* Projekti ne lenden "Siguria e te dhenave". Realizimi i funksioneve u implmentua ne gjuhen programuese C#

* Per udhezimet rreth ekzekutimit te programit ne Windows, si dhe rezultatin e ekzekutimeve referohu tek 'Raporti i projektit - Udhezuesi'


1. Ekzekutimi i Programit ne Linux ose Mac(ne Windows referohu tek Raporti): 

* Supozojme se jemi ne pathin e duhur atehere:

* Ne menyre qe te behet ekzekutimi i funksioneve duhet te instalojme open-source platformen Mono e dizajnuar per Linux dhe Mac e bazuar ne .Net Framework! 


* Hapi 1: Kompailimi ne Terminal behet duke përdorur komandën  "msbuild ds.sln" per te ndertuar projektin
* Hapi 2: Ekzekutimi dhe thirrja e main behet ne pathin ku ekziston ds.dll file duke perdorum komanden "mono ds.dll" pastaj therrasim argumentet si me poshte: =>

	1.Ekzekutimi i funksionit Beale:
		mono ds.dll Beale Encrypt  <text>
     		mono ds.dll Beale Decrypt  <text>

	2.Ekzekutimi i funksionit Permutation:
		mono ds.dll Permutation Encrypt  <key><text>
     		mono ds.dll Permutation Decrypt  <key><text>

	3.Ekzekutimi i funksionit Numerical:
		mono ds.dll Numerical Encode  <text>
     		mono ds.dll Numerical Decode  <code>

2. Pershkrimi i Komandave: 

* Komanda Beale: 	Zevendson secilin shkronje te  Plaintextit me poziten e saj ne nje liber.

*  Komanda Permutation: Transformon Plaintextin(i cili ndahet ne blloqe) ne baze te nje permutacioni me ane te qelsit te dhene. Gjatesia e qelesit duhet domosdo te jete sa gjatesia e bloqeve.
Qelsi duhet te permbaje numra jo te persitur. Ne algoritmin tone Plaintext eshte i ndare ne blloqe nga 4 keshtu qe edhe qelsi mundet te jete vetem si permutacion i numrave 1234, ku secili numer i pergjigjet pozites se plaintextit ne bllok.

* Komanda Numerical:    Secila shkronjë zëvendësohet me pozitën e saj në alfabetin e gjuhes angleze. Kështu shkronja “a” enkodohet në numrin 1, shkronja “b” në numrin 2 e keshtu me radhe.

3. Per te pare Rezultatet e ekzekutimit referohu tek 'Raporti i projektit'!









