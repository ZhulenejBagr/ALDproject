# ALDProject
Autor: Ondřej Šimůnek

Aplikace je napsaná v C#, .NET 7.0., GUI využívá webové rozhraní ASP.NET. Aplikaci jsem psal sám. 

---

## Algoritmus a detaily implementace

Algoritmus na generaci dlaždic pracuje s `N`*`M` maticí celých čísel, kde hodnota dlaždice reprezentuje v jakých směrech je dlaždice propojená:
* `-2` žádná kompatibilní dlaždice (současně nevyužito)
* `-1` dlaždice ještě nemá nastavenou hodnotu
* `0-15` dlaždice má validní hodnotu a jednotlivé bity reprezentují propojení (nejmenší bit je propojení nahoře, další bity jsou po směru hodinových ručiček - druhý nejmenší vpravo, třetí nejmenší dole, čtvrtý nejmenší vlevo)

Generace začíná v prostředku gridu a pokračuje směrem ven, pořadí je určeno vzdáleností od počátku vzestupně.

Generaci lze ovlivnit parametrem *seed*, který je zodpovědný za nastavení spojení ještě nevygenerovaných. Zároveň lze nastavit velikost gridu pomocí parametrů *xdim* a *ydim*, které mají omezení ve formě minima 3 a maxima 50.

Logika rozhodnutí o spojení dlaždic je jednoduchá:
* pokud je sousední dlaždice ve směru *x* nastavená, spojení v tomto směru bude odpovídat spojení této dlaždice ve směru opačné *x*. (nastavená dlaždice vpravo od současné určí hodnotu spojení doprava svým spojením doleva)
* pokud sousední dlaždice není nastavená, spojení se rozhodne na základě pseudonáhodného výběru.

Současná implementace pracuje se všemi možnými dlaždicemi, logiku na omezení této množiny by bylo lehké přidělat, jenom GUI se mi nechtělo dělat.

---
## GUI a práce s aplikací

GUI obsahuje tři input boxy a tlačítko na vygenerování gridu. Input boxy slouží pro nastavení parametrů generace, nejsou na nich ošetřené vstupy (považuji za nepodstatné vůči zadání). Po stisknutí tlačítka se vygeneruje grid s parametry, které byly v moment stisknutí tlačítka zadané. Vzhled dlaždic jsem dělal vlastnoručně sám. GUI je kost a kůže protože grafický design není můj passion.

![image](https://user-images.githubusercontent.com/43739606/211933236-79609e73-35ef-4b60-9e7a-a779c23ef5b5.png)
