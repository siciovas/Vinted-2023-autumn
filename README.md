# Vinted BackEnd Assignment

Here, at Vinted, our members call themselves 'Vinties' and shopping at Vinted even has its term - 'to vint'. And our members do vint a lot. Naturally, when something is purchased, it has to be shipped and Vinted provides various shipping options to its members across the globe. However, let's focus on France. In France, it is allowed to ship via either 'Mondial Relay' (MR in short) or 'La Poste' (LP). While 'La Poste' provides usual courier delivery services, 'Mondial Relay' allows you to drop and pick up a shipment at a so-called drop-off point, thus being less convenient, but cheaper for larger packages.

Each item, depending on its size gets an appropriate package size assigned to it:

* S - Small, a popular option to ship jewelry
* M - Medium - clothes and similar items
* L - Large - mostly shoes
Shipping price depends on package size and provider:
Shipping price depends on package size and provider:

| Provider | Package Size | Price  |
| -------- | ------------ | ------ |
| LP       | S            | 1.50 € |
| LP       | M            | 4.90 € |
| LP	     | L	          | 6.90 € |
| MR	     | S	          | 2 €    |
| MR	     | M 	          | 3 €    |
| MR	     | L	          | 4 €    |

Usually, the shipping price is covered by the buyer, but sometimes, to promote one or another provider, Vinted covers part of the shipping price.

Your task is to create a shipment discount calculation module.

First, you have to implement such rules:

All S shipments should always match the lowest S package price among the providers.
The third L shipment via LP should be free, but only once a calendar month.
Accumulated discounts cannot exceed 10 € in a calendar month. If there are not enough funds to fully cover a discount this calendar month, it should be covered partially.
Your design should be flexible enough to allow adding new rules and modifying existing ones easily.

Member's transactions are listed in a file 'input.txt', each line containing: the date (without hours, in ISO format), package size code, and carrier code, separated with whitespace:

```
2015-02-01 S MR
2015-02-02 S MR
2015-02-03 L LP
2015-02-05 S LP
2015-02-06 S MR
2015-02-06 L LP
2015-02-07 L MR
2015-02-08 M MR
2015-02-09 L LP
2015-02-10 L LP
2015-02-10 S MR
2015-02-10 S MR
2015-02-11 L LP
2015-02-12 M MR
2015-02-13 M LP
2015-02-15 S MR
2015-02-17 L LP
2015-02-17 S MR
2015-02-24 L LP
2015-02-29 CUSPS
2015-03-01 S MR
```

Your program should output transactions and append a reduced shipment price and a shipment discount (or '-' if there is none). The program should append an 'Ignored' word if the line format is wrong or carrier/sizes are unrecognized.

```
2015-02-01 S MR 1.50 0.50
2015-02-02 S MR 1.50 0.50
2015-02-03 L LP 6.90 -
2015-02-05 S LP 1.50 -
2015-02-06 S MR 1.50 0.50
2015-02-06 L LP 6.90 -
2015-02-07 L MR 4.00 -
2015-02-08 M MR 3.00 -
2015-02-09 L LP 0.00 6.90
2015-02-10 L LP 6.90 -
2015-02-10 S MR 1.50 0.50
2015-02-10 S MR 1.50 0.50
2015-02-11 L LP 6.90 -
2015-02-12 M MR 3.00 -
2015-02-13 M LP 4.90 -
2015-02-15 S MR 1.50 0.50
2015-02-17 L LP 6.90 -
2015-02-17 S MR 1.90 0.10
2015-02-24 L LP 6.90 -
2015-02-29 CUSPS Ignored
2015-03-01 S MR 1.50 0.50
```
