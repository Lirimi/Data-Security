# ds
* Projekti ne lenden "Siguria e te dhenave". Realizimi i funksioneve u implmentua ne gjuhen programuese C#

* Per udhezimet rreth ekzekutimit te programit ne Windows, si dhe rezultatin e ekzekutimeve referohu tek 'Raporti i projektit';





1. Ekzekutimi i Programit ne Linux(ne Windows referohu tek Raporti): 

* Supozojme se jemi ne pathin e duhur atehere:

* Kompailimi ne Terminal behet duke përdorur komandën:  csc Program.cs
  
* Ne menyre qe te behet ekzekutimi i funksioneve duhet te instalojme open-source platformen Mono e dizajnuar per Linux dhe Mac e bazuar ne .Net Framework! 

	1.Ekzekutimi i funksionit Beale:
		mono Program.exe Beale Encrypt  <text>
     		mono Program.exe Beale Decrypt  <text>

	2.Ekzekutimi i funksionit Permutation:
		mono Program.exe Permutation Encrypt  <key><text>
     		mono Program.exe Permutation Decrypt  <key><text>

	3.Ekzekutimi i funksionit Numerical:
		mono Program.exe Numerical Encode  <text>
     		mono Program.exe Numerical Decode  <code>

2. Pershkrimi i Komandave: 

* Komanda Beale: Zevendson secilin shkronje te  Plaintextit me poziten e saj ne nje liber.

  

*  Komanda Permutation: Transformon Plaintextin(i cili ndahet ne blloqe) ne baze te nje permutacioni me ane te qelsit te dhene. Gjatesia e qelesit duhet domosdo te jete sa gjatesia e bloqeve.
Qelsi duhet te permbaje numra jo te persitur. Ne algoritmin tone Plaintext eshte i ndare ne blloqe nga 4 keshtu qe edhe qelsi mundet te jete vetem si permutacion i numrave 1234, ku secili numer i pergjigjet pozites se plaintextit ne bllok.

* Komanda Numerical:    Secila shkronjë zëvendësohet me pozitën e saj në alfabetin e gjuhes angleze. Kështu shkronja “a” enkodohet në numrin 1, shkronja “b” në numrin 2 e keshtu me radhe.

3. Per te pare Rezultatet e ekzekutimit referohu tek 'Raporti i projektit'!









