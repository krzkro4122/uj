class Script2 {
    funkcjaKwadratowa(a, b, c) {
        const delta = b ** 2 - 4 * a * c;
        if (delta < 0)
            return "Brak prawdziwych rozwiazan!"
        else if (delta == 0)
            return -b / (2 * a);
        else {
            const x1 = (-b - Math.sqrt(delta)) / (2 * a)
            const x2 = (-b + Math.sqrt(delta)) / (2 * a)
            return [x1, x2]
        }
    }

    zapiszWTablicy(tablica) {
        return tablica.reverse();
    }

    objetoscSzescianu(bok) {
        if (bok <= 0) return "Bok szczescianu nie moze miec takiej wartosci!"
        return bok ** 3
    }

    sumaCyfr(liczba) {
        if (liczba <= 0) return "podana liczba nie jest dodatnia"

        const parsedNumber = `${liczba}`;
        if (parsedNumber.length !== 3) return "podana liczba nie jest trzycyfrowa"

        const digits = [parsedNumber[0], parsedNumber[1], parsedNumber[2]]
        let sum = 0;
        for (const digit of digits)
            sum += digit

        return sum % 2 != 0 ? "liczba jest parzysta" : "liczba jest nieparzysta"
    }

    zamienElementy(tablica, indeks1, indeks2, liczba) {
        const output = [...tablica];
        if (indeks1 >= tablica.length || indeks2 >= tablica.length) return "indeks spoza zakresu"
        if (liczba > 0) {
            const temporary = output[indeks1]
            output[indeks1] = output[indeks2]
            output[indeks2] = temporary
        }
        return output
    }
}
