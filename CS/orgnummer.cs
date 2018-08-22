using static System.Math;

public class StringToDecimalResult{
	public double result;
	public char [] feilmelding;
	public bool success;
}
public class BooleanReference{
	public bool booleanValue;
}
public class DoubleReference{
	public double doubleValue;
}
public class StringReference{
	public char [] stringx;
}
public class DecimalListRef{
	public double [] list;
}
public class StringListRef{
	public StringReference [] list;
}
public class orgnummer{
	public static bool erGyldigOrgNummer(char [] orgnummer){
		bool gyldig;
		double d1, d2, d3, d4, d5, d6, d7, d8, d9;
		double sum, rest, kontrollsiffer;
		DoubleReference failures;

		if(orgnummer.Length == 9d){
			failures = new DoubleReference();
			failures.doubleValue = 0d;
			d1 = charToNumber(orgnummer[0], failures);
			d2 = charToNumber(orgnummer[1], failures);
			d3 = charToNumber(orgnummer[2], failures);
			d4 = charToNumber(orgnummer[3], failures);
			d5 = charToNumber(orgnummer[4], failures);
			d6 = charToNumber(orgnummer[5], failures);
			d7 = charToNumber(orgnummer[6], failures);
			d8 = charToNumber(orgnummer[7], failures);
			d9 = charToNumber(orgnummer[8], failures);
			if(failures.doubleValue == 0d){
				sum = d1*3d + d2*2d + d3*7d + d4*6d + d5*5d + d6*4d + d7*3d + d8*2d;
				rest = sum%11d;
				kontrollsiffer = 11d - rest;
				if(rest != 1d && kontrollsiffer == d9){
					gyldig = true;
				}else{
					gyldig = false;
				}
			}else{
				gyldig = false;
			}
		}else{
			gyldig = false;
		}

		return gyldig;
	}


	public static double charToNumber(char c, DoubleReference failures){
		double val;

		val = getDecimalFromSingleDecimalDigit(c, 10d);

		if(val > 0d && val <= 9d){
		}else{
			failures.doubleValue = failures.doubleValue + 1d;
		}

		return val;
	}


	public static double test(){
		double failures;
		bool gyldig;

		failures = 0d;

		gyldig = erGyldigOrgNummer("913965639".ToCharArray());

		if(!gyldig){
			failures = failures + 1d;
		}

		gyldig = erGyldigOrgNummer("913965638".ToCharArray());

		if(gyldig){
			failures = failures + 1d;
		}

		return failures;
	}


	public static char [] decimalToString(double decimalx){

		return numberToString(decimalx, 10d);
	}


	public static char [] numberToString(double decimalx, double basex){
		char [] stringx;
		double digits;
		double digitPosition;
		bool hasPrintedPoint;
		double i, d;

		stringx = new char [0];

		digits = getDigits(basex);

		/* Find digitPosition:*/
		digitPosition = getDigitPosition(decimalx, basex);

		decimalx = (double)Round(decimalx*Pow(basex, -digitPosition  + digits - 1d));

		hasPrintedPoint = false;

		/* Print leading zeros.*/
		if(digitPosition < 0d){
			stringx = appendCharacter(stringx, '0');
			stringx = appendCharacter(stringx, '.');
			hasPrintedPoint = true;
			for(i = 0d; i < -digitPosition  - 1d; i = i + 1d){
				stringx = appendCharacter(stringx, '0');
			}
		}

		/* Print number.*/
		for(i = 0d; i < digits; i = i + 1d){
			d = Floor(decimalx/Pow(basex, digits - i - 1d));
			if(!hasPrintedPoint && digitPosition - i + 1d == 0d){
				if(decimalx != 0d){
					stringx = appendCharacter(stringx, '.');
				}
				hasPrintedPoint = true;
			}
			if(decimalx == 0d && hasPrintedPoint){
			}else{
				stringx = appendCharacter(stringx, getSingleDigitFromNumber(d, basex));
			}
			decimalx = decimalx - d*Pow(basex, digits - i - 1d);
		}

		/* Print trailing zeros.*/
		for(i = 0d; i < digitPosition - digits + 1d; i = i + 1d){
			stringx = appendCharacter(stringx, '0');
		}

		/* Done*/
		return stringx;
	}


	public static double getDigits(double basex){
		double t;

		t = Pow(10d, 15d);
		return Floor(Log10(t)/Log10(basex));
	}


	public static double getDigitPosition(double decimalx, double basex){
		double power;
		double t;

		power = Ceiling(Log10(decimalx)/Log10(basex));

		t = decimalx*Pow(basex, -power );
		if(t < basex && t >= 1d){
		}else if(t >= basex){
			power = power + 1d;
		}else if(t < 1d){
			power = power - 1d;
		}


		return power;
	}


	public static char getSingleDigitFromNumber(double c, double basex){
		char [] numberTable;
		char retc;

		numberTable = getNumberTable();

		if(c > basex - 1d){
			retc = '?';
		}else{
			retc = numberTable[(int)(c)];
		}

		return retc;
	}


	public static char [] getNumberTable(){
		char [] numberTable;

		numberTable = new char [16];
		numberTable[0] = '0';
		numberTable[1] = '1';
		numberTable[2] = '2';
		numberTable[3] = '3';
		numberTable[4] = '4';
		numberTable[5] = '5';
		numberTable[6] = '6';
		numberTable[7] = '7';
		numberTable[8] = '8';
		numberTable[9] = '9';
		numberTable[10] = 'A';
		numberTable[11] = 'B';
		numberTable[12] = 'C';
		numberTable[13] = 'D';
		numberTable[14] = 'E';
		numberTable[15] = 'F';
		return numberTable;
	}


	public static StringToDecimalResult stringToDecimal(char [] stringx){

		return stringToDecimalForBase(stringx, 10d);
	}


	public static StringToDecimalResult stringToDecimalForBase(char [] stringx, double basex){
		StringToDecimalResult stringToDecimalResult;
		char c;
		double i, j;
		bool isPositive;
		double beforeDecimalPoint;
		double afterDecimalPoint;
		double n;
		double validCharacters;
		double d;

		stringToDecimalResult = new StringToDecimalResult();
		stringToDecimalResult.success = true;
		i = 0d;
		isPositive = true;
		beforeDecimalPoint = 0d;
		afterDecimalPoint = 0d;
		n = 0d;
		validCharacters = 0d;

		if(basex >= 2d && basex <= 16d){
			for(j = 0d; j < stringx.Length; j = j + 1d){
				c = stringx[(int)(j)];
				if(isNumber(c, basex) || c == '.' || c == '-'){
					validCharacters = validCharacters + 1d;
				}
			}
			if(validCharacters == stringx.Length){
				if(stringx.Length > 0d){
					c = stringx[(int)(i)];
					if(c == '-'){
						isPositive = false;
						i = i + 1d;
					}
					if(i < stringx.Length){
						c = stringx[(int)(i)];
						if(isNumber(c, basex)){
							for(; isNumber(c, basex) && (i < stringx.Length); ){
								beforeDecimalPoint = beforeDecimalPoint + 1d;
								i = i + 1d;
								if(i < stringx.Length){
									c = stringx[(int)(i)];
								}
							}
							if(i < stringx.Length){
								c = stringx[(int)(i)];
								if(c == '.'){
									i = i + 1d;
									if(i < stringx.Length){
										c = stringx[(int)(i)];
										for(; isNumber(c, basex) && (i < stringx.Length); ){
											afterDecimalPoint = afterDecimalPoint + 1d;
											i = i + 1d;
											if(i < stringx.Length){
												c = stringx[(int)(i)];
											}
										}
									}else{
										stringToDecimalResult.success = false;
										stringToDecimalResult.feilmelding = "Number must have digits after the decimal point.".ToCharArray();
									}
								}
							}
						}else{
							stringToDecimalResult.success = false;
							stringToDecimalResult.feilmelding = "Number must start with digits (for negative numbers, after the optional negative sign).".ToCharArray();
						}
					}
					if(stringToDecimalResult.success != false){
						i = 0d;
						if(!isPositive){
							i = 1d;
						}
						for(j = 0d; j < beforeDecimalPoint; j = j + 1d){
							c = stringx[(int)(i)];
							i = i + 1d;
							d = getDecimalFromSingleDecimalDigit(c, basex);
							n = n + d*Pow(basex, beforeDecimalPoint - j - 1d);
						}
						if(afterDecimalPoint > 0d){
							i = i + 1d;
							for(j = 0d; j < afterDecimalPoint; j = j + 1d){
								c = stringx[(int)(i)];
								i = i + 1d;
								d = getDecimalFromSingleDecimalDigit(c, basex);
								n = n + d*Pow(basex, 0d - j - 1d);
							}
						}
						if(!isPositive){
							n = -n ;
						}
						stringToDecimalResult.result = n;
						stringToDecimalResult.success = true;
					}
				}else{
					stringToDecimalResult.success = false;
					stringToDecimalResult.feilmelding = "String has no content.".ToCharArray();
				}
			}else{
				stringToDecimalResult.success = false;
				stringToDecimalResult.feilmelding = "String contains invalid character.".ToCharArray();
			}
		}else{
			stringToDecimalResult.success = false;
			stringToDecimalResult.feilmelding = "Base must be from 2 to 16.".ToCharArray();
		}

		return stringToDecimalResult;
	}


	public static double getDecimalFromSingleDecimalDigit(char c, double basex){
		char [] numberTable;
		double i;
		double position;

		numberTable = getNumberTable();
		position = 0d;

		for(i = 0d; i < basex; i = i + 1d){
			if(numberTable[(int)(i)] == c){
				position = i;
			}
		}

		return position;
	}


	public static bool isNumber(char c, double basex){
		char [] numberTable;
		double i;
		bool found;

		numberTable = getNumberTable();
		found = false;

		for(i = 0d; i < basex; i = i + 1d){
			if(numberTable[(int)(i)] == c){
				found = true;
			}
		}

		return found;
	}


	public static void assertFalse(bool b, DoubleReference failures){

		if(b){
			failures.doubleValue = failures.doubleValue + 1d;
		}
	}


	public static void assertTrue(bool b, DoubleReference failures){

		if(!b){
			failures.doubleValue = failures.doubleValue + 1d;
		}
	}


	public static void assertEquals(double a, double b, DoubleReference failures){

		if(a != b){
			failures.doubleValue = failures.doubleValue + 1d;
		}
	}


	public static void assertStringEquals(char [] a, char [] b, DoubleReference failures){

		if(!stringsEqual(a, b)){
			failures.doubleValue = failures.doubleValue + 1d;
		}
	}


	public static double [] stringToNumberArray(char [] stringx){
		double i;
		double [] array;

		array = new double [(int)(stringx.Length)];

		for(i = 0d; i < stringx.Length; i = i + 1d){
			array[(int)(i)] = stringx[(int)(i)];
		}
		return array;
	}


	public static char [] numberArrayToString(double [] array){
		double i;
		char [] stringx;

		stringx = new char [(int)(array.Length)];

		for(i = 0d; i < array.Length; i = i + 1d){
			stringx[(int)(i)] = (char)(array[(int)(i)]);
		}
		return stringx;
	}


	public static bool stringsEqual(char [] data1, char [] data2){
		bool equal;
		double nrEqual, i;

		equal = false;
		if(data1.Length == data2.Length){
			nrEqual = 0d;
			for(i = 0d; i < data1.Length; i = i + 1d){
				if(data1[(int)(i)] == data2[(int)(i)]){
					nrEqual = nrEqual + 1d;
				}
			}
			if(nrEqual == data1.Length){
				equal = true;
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static bool numberArraysEqual(double [] data1, double [] data2){
		bool equal;
		double nrEqual, i;

		equal = false;
		if(data1.Length == data2.Length){
			nrEqual = 0d;
			for(i = 0d; i < data1.Length; i = i + 1d){
				if(data1[(int)(i)] == data2[(int)(i)]){
					nrEqual = nrEqual + 1d;
				}
			}
			if(nrEqual == data1.Length){
				equal = true;
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static char [] substring(char [] stringx, double from, double to){
		char [] n;
		double i;

		n = new char [(int)(to - from)];

		for(i = from; i < to; i = i + 1d){
			n[(int)(i - from)] = stringx[(int)(i)];
		}

		return n;
	}


	public static char [] appendString(char [] stringx, char [] s){
		char [] newString;
		double i;

		newString = new char [(int)(stringx.Length + s.Length)];

		for(i = 0d; i < stringx.Length; i = i + 1d){
			newString[(int)(i)] = stringx[(int)(i)];
		}

		for(i = 0d; i < s.Length; i = i + 1d){
			newString[(int)(stringx.Length + i)] = s[(int)(i)];
		}

		delete(stringx);

		return newString;
	}


	public static char [] appendCharacter(char [] stringx, char c){
		char [] newString;
		double i;

		newString = new char [(int)(stringx.Length + 1d)];

		for(i = 0d; i < stringx.Length; i = i + 1d){
			newString[(int)(i)] = stringx[(int)(i)];
		}

		newString[(int)(stringx.Length)] = c;

		delete(stringx);

		return newString;
	}


	public static StringReference [] split(char [] toSplit, char splitBy){
		StringReference [] splitt;
		char [] next;
		double i;
		char c;
		StringReference n;

		splitt = new StringReference [0];

		next = new char [0];
		for(i = 0d; i < toSplit.Length; i = i + 1d){
			c = toSplit[(int)(i)];
			if(c == splitBy){
				n = new StringReference();
				n.stringx = next;
				splitt = addString(splitt, n);
				next = new char [0];
			}else{
				next = appendCharacter(next, c);
			}
		}

		if(next.Length > 0d){
			n = new StringReference();
			n.stringx = next;
			splitt = addString(splitt, n);
		}

		return splitt;
	}


	public static double [] addDecimal(double [] list, double a){
		double [] newlist;
		double i;

		newlist = new double [(int)(list.Length + 1d)];
		for(i = 0d; i < list.Length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.Length)] = a;
		
		delete(list);
		
		return newlist;
	}


	public static void addDecimalRef(DecimalListRef list, double i){

		list.list = addDecimal(list.list, i);
	}


	public static double [] removeDecimal(double [] list, double n){
		double [] newlist;
		double i;

		newlist = new double [(int)(list.Length - 1d)];

		for(i = 0d; i < list.Length; i = i + 1d){
			if(i < n){
				newlist[(int)(i)] = list[(int)(i)];
			}
			if(i > n){
				newlist[(int)(i - 1d)] = list[(int)(i)];
			}
		}
		
		delete(list);
		
		return newlist;
	}


	public static double getDecimalRef(DecimalListRef list, double i){

		return list.list[(int)(i)];
	}


	public static void removeDecimalRef(DecimalListRef list, double i){

		list.list = removeDecimal(list.list, i);
	}


	public static StringReference [] addString(StringReference [] list, StringReference a){
		StringReference [] newlist;
		double i;

		newlist = new StringReference [(int)(list.Length + 1d)];

		for(i = 0d; i < list.Length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.Length)] = a;
		
		delete(list);
		
		return newlist;
	}


	public static void addStringRef(StringListRef list, StringReference i){

		list.list = addString(list.list, i);
	}


	public static StringReference [] removeString(StringReference [] list, double n){
		StringReference [] newlist;
		double i;

		newlist = new StringReference [(int)(list.Length - 1d)];

		for(i = 0d; i < list.Length; i = i + 1d){
			if(i < n){
				newlist[(int)(i)] = list[(int)(i)];
			}
			if(i > n){
				newlist[(int)(i - 1d)] = list[(int)(i)];
			}
		}
		
		delete(list);
		
		return newlist;
	}


	public static StringReference getStringRef(StringListRef list, double i){

		return list.list[(int)(i)];
	}


	public static void removeStringRef(StringListRef list, double i){

		list.list = removeString(list.list, i);
	}


	public static void delete(System.Object objectx){
		// C# has garbage collection.
	}
}


public class orgnummerTestBundle{

	public static void Main(){
		System.Console.WriteLine(orgnummer.test());
	}
}