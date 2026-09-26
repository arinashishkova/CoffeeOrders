# Coffee Orders

WPF-rakendus kohviku tellimuste haldamiseks.

## Funktsioonid

- Kohvitellimuste lisamine
- Valitud tellimuse muutmine
- Tellimuste kustutamine kinnitusega
- Tellimuste otsimine kliendi nime järgi
- Tellimuste kuvamine DataGridis
- Hinna ja valmistusaja arvutamine
- Kasutaja sisendi kontrollimine

## Projekti struktuur

- `CoffeeOrders.Core` - andmetüübid, arvutused ja ärireeglid
- `CoffeeOrders.WpfApp` - WPF kasutajaliides

## Eeldused

- Kliendi nimi peab sisaldama 2-30 märki.
- Kogus peab olema vahemikus 1-10.
- Espresso jaoks ei ole Large suurus lubatud.
- Rakendus saab hoida kuni 100 tellimust.
- Hind ja valmistusaeg sõltuvad joogi tüübist ja suurusest.

## Kontrollnäited

### Näide 1

Klient: Anna  
Jook: Latte  
Suurus: Medium  
Kogus: 2  

Oodatav tulemus:
- Hind: 7.00 €
- Valmistusaeg: 8 min

### Näide 2

Klient: Mark  
Jook: Espresso  
Suurus: Small  
Kogus: 1  

Oodatav tulemus:
- Hind: 2.00 €
- Valmistusaeg: 2 min

### Näide 3

Klient: John  
Jook: Espresso  
Suurus: Large  
Kogus: 1  

Oodatav tulemus:
- Tellimust ei lisata, sest Large suurus ei ole Espresso jaoks lubatud.

## Käivitamine

Ava lahendus Visual Studios, määra `CoffeeOrders.WpfApp` käivitusprojektiks, ehita lahendus ja käivita rakendus.