describe('Script2', function () {
  let script2;

  beforeEach(function () {
    script2 = new Script2();
  });


  describe('funkcjaKwadratowa', function () {
    it('should compute a quadratic equation1', function () {
      const output = script2.funkcjaKwadratowa(1, 2, -3)
      expect(output).toEqual([-3, 1]);
    });

    it('should compute a quadratic equation2', function () {
      const output = script2.funkcjaKwadratowa(1, 2, 1)
      expect(output).toEqual(-1);
    });

    it('should compute a quadratic equation3', function () {
      const output = script2.funkcjaKwadratowa(1, 1, 1)
      expect(output).toEqual("Brak prawdziwych rozwiazan!");
    });
  });


  describe('zapiszWTablicy', function () {
    it('should reverse an array1', function () {
      const output = script2.zapiszWTablicy([1, 2, 3])
      expect(output).toEqual([3, 2, 1]);
    });

    it('should reverse an array2', function () {
      const output = script2.zapiszWTablicy(['a', 'b', 'c'])
      expect(output).toEqual(['c', 'b', 'a']);
    });
  });


  describe('objetoscSzescianu', function () {
    it('should compute the volume of a cube1', function () {
      const output = script2.objetoscSzescianu(1)
      expect(output).toEqual(1);
    });

    it('should compute the volume of a cube2', function () {
      const output = script2.objetoscSzescianu(0)
      expect(output).toEqual("Bok szczescianu nie moze miec takiej wartosci!");
    });
  });


  describe('sumaCyfr', function () {
    it('should describe the number as1', function () {
      const output = script2.sumaCyfr(123)
      expect(output).toEqual("liczba jest parzysta");
    });

    it('should describe the number as2', function () {
      const output = script2.sumaCyfr(124)
      expect(output).toEqual("liczba jest nieparzysta");
    });

    it('should describe the number as3', function () {
      const output = script2.sumaCyfr(1)
      expect(output).toEqual("podana liczba nie jest trzycyfrowa");
    });

    it('should describe the number as4', function () {
      const output = script2.sumaCyfr(0)
      expect(output).toEqual("podana liczba nie jest dodatnia");
    });
  });


  describe('zamienElementy', function () {
    it('should conditionally swap array elements1', function () {
      const output = script2.zamienElementy([1, 2, 3, 4], 0, 1, 1)
      expect(output).toEqual([2, 1, 3, 4]);
    });

    it('should conditionally swap array elements2', function () {
      const output = script2.zamienElementy([1, 2, 3, 4], 0, 1, 0)
      expect(output).toEqual([1, 2, 3, 4]);
    });

    it('should conditionally swap array elements3', function () {
      const output = script2.zamienElementy([1, 2, 3, 4], 0, 10, 0)
      expect(output).toEqual("indeks spoza zakresu");
    });
  });
});
