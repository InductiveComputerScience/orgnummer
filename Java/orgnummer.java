import static java.lang.Math.*;

public class orgnummer{
	public static boolean erGyldigOrgNummer(char [] orgnummer){
		boolean gyldig;
		double d1, d2, d3, d4, d5, d6, d7, d8, d9;
		double sum, rest, kontrollsiffer;
		DoubleReference failures;

		if(orgnummer.length == 9d){
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
		boolean gyldig;

		failures = 0d;

		gyldig = erGyldigOrgNummer("913965639".toCharArray());

		if(!gyldig){
			failures = failures + 1d;
		}

		gyldig = erGyldigOrgNummer("913965638".toCharArray());

		if(gyldig){
			failures = failures + 1d;
		}

		return failures;
	}

static public class StringToDecimalResult{
	public double result;
	public char [] feilmelding;
	public boolean success;
}
	public static char [] decimalToString(double decimal){

		return numberToString(decimal, 10d);
	}

	public static char [] numberToString(double decimal, double base){
		char [] string;
		double digits;
		double digitPosition;
		boolean hasPrintedPoint;
		double i, d;

		string = new char [0];

		digits = getDigits(base);

		/* Find digitPosition:*/
		digitPosition = getDigitPosition(decimal, base);

		decimal = (double)round(decimal*pow(base, -digitPosition  + digits - 1d));

		hasPrintedPoint = false;

		/* Print leading zeros.*/
		if(digitPosition < 0d){
			string = appendCharacter(string, '0');
			string = appendCharacter(string, '.');
			hasPrintedPoint = true;
			for(i = 0d; i < -digitPosition  - 1d; i = i + 1d){
				string = appendCharacter(string, '0');
			}
		}

		/* Print number.*/
		for(i = 0d; i < digits; i = i + 1d){
			d = floor(decimal/pow(base, digits - i - 1d));
			if(!hasPrintedPoint && digitPosition - i + 1d == 0d){
				if(decimal != 0d){
					string = appendCharacter(string, '.');
				}
				hasPrintedPoint = true;
			}
			if(decimal == 0d && hasPrintedPoint){
			}else{
				string = appendCharacter(string, getSingleDigitFromNumber(d, base));
			}
			decimal = decimal - d*pow(base, digits - i - 1d);
		}

		/* Print trailing zeros.*/
		for(i = 0d; i < digitPosition - digits + 1d; i = i + 1d){
			string = appendCharacter(string, '0');
		}

		/* Done*/
		return string;
	}

	public static double getDigits(double base){
		double t;

		t = pow(10d, 15d);
		return floor(log10(t)/log10(base));
	}

	public static double getDigitPosition(double decimal, double base){
		double power;
		double t;

		power = ceil(log10(decimal)/log10(base));

		t = decimal*pow(base, -power );
		if(t < base && t >= 1d){
		}else if(t >= base){
			power = power + 1d;
		}else if(t < 1d){
			power = power - 1d;
		}


		return power;
	}

	public static char getSingleDigitFromNumber(double c, double base){
		char [] numberTable;
		char retc;

		numberTable = getNumberTable();

		if(c > base - 1d){
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

	public static StringToDecimalResult stringToDecimal(char [] string){

		return stringToDecimalForBase(string, 10d);
	}

	public static StringToDecimalResult stringToDecimalForBase(char [] string, double base){
		StringToDecimalResult stringToDecimalResult;
		char c;
		double i, j;
		boolean isPositive;
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

		if(base >= 2d && base <= 16d){
			for(j = 0d; j < string.length; j = j + 1d){
				c = string[(int)(j)];
				if(isNumber(c, base) || c == '.' || c == '-'){
					validCharacters = validCharacters + 1d;
				}
			}
			if(validCharacters == string.length){
				if(string.length > 0d){
					c = string[(int)(i)];
					if(c == '-'){
						isPositive = false;
						i = i + 1d;
					}
					if(i < string.length){
						c = string[(int)(i)];
						if(isNumber(c, base)){
							for(; isNumber(c, base) && (i < string.length); ){
								beforeDecimalPoint = beforeDecimalPoint + 1d;
								i = i + 1d;
								if(i < string.length){
									c = string[(int)(i)];
								}
							}
							if(i < string.length){
								c = string[(int)(i)];
								if(c == '.'){
									i = i + 1d;
									if(i < string.length){
										c = string[(int)(i)];
										for(; isNumber(c, base) && (i < string.length); ){
											afterDecimalPoint = afterDecimalPoint + 1d;
											i = i + 1d;
											if(i < string.length){
												c = string[(int)(i)];
											}
										}
									}else{
										stringToDecimalResult.success = false;
										stringToDecimalResult.feilmelding = "Number must have digits after the decimal point.".toCharArray();
									}
								}
							}
						}else{
							stringToDecimalResult.success = false;
							stringToDecimalResult.feilmelding = "Number must start with digits (for negative numbers, after the optional negative sign).".toCharArray();
						}
					}
					if(stringToDecimalResult.success != false){
						i = 0d;
						if(!isPositive){
							i = 1d;
						}
						for(j = 0d; j < beforeDecimalPoint; j = j + 1d){
							c = string[(int)(i)];
							i = i + 1d;
							d = getDecimalFromSingleDecimalDigit(c, base);
							n = n + d*pow(base, beforeDecimalPoint - j - 1d);
						}
						if(afterDecimalPoint > 0d){
							i = i + 1d;
							for(j = 0d; j < afterDecimalPoint; j = j + 1d){
								c = string[(int)(i)];
								i = i + 1d;
								d = getDecimalFromSingleDecimalDigit(c, base);
								n = n + d*pow(base, 0d - j - 1d);
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
					stringToDecimalResult.feilmelding = "String has no content.".toCharArray();
				}
			}else{
				stringToDecimalResult.success = false;
				stringToDecimalResult.feilmelding = "String contains invalid character.".toCharArray();
			}
		}else{
			stringToDecimalResult.success = false;
			stringToDecimalResult.feilmelding = "Base must be from 2 to 16.".toCharArray();
		}

		return stringToDecimalResult;
	}

	public static double getDecimalFromSingleDecimalDigit(char c, double base){
		char [] numberTable;
		double i;
		double position;

		numberTable = getNumberTable();
		position = 0d;

		for(i = 0d; i < base; i = i + 1d){
			if(numberTable[(int)(i)] == c){
				position = i;
			}
		}

		return position;
	}

	public static boolean isNumber(char c, double base){
		char [] numberTable;
		double i;
		boolean found;

		numberTable = getNumberTable();
		found = false;

		for(i = 0d; i < base; i = i + 1d){
			if(numberTable[(int)(i)] == c){
				found = true;
			}
		}

		return found;
	}

	public static void assertFalse(boolean b, DoubleReference failures){

		if(b){
			failures.doubleValue = failures.doubleValue + 1d;
		}
	}

	public static void assertTrue(boolean b, DoubleReference failures){

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

	public static double [] stringToNumberArray(char [] string){
		double i;
		double [] array;

		array = new double [(int)(string.length)];

		for(i = 0d; i < string.length; i = i + 1d){
			array[(int)(i)] = string[(int)(i)];
		}
		return array;
	}

	public static char [] numberArrayToString(double [] array){
		double i;
		char [] string;

		string = new char [(int)(array.length)];

		for(i = 0d; i < array.length; i = i + 1d){
			string[(int)(i)] = (char)(array[(int)(i)]);
		}
		return string;
	}

	public static boolean stringsEqual(char [] data1, char [] data2){
		boolean equal;
		double nrEqual, i;

		equal = false;
		if(data1.length == data2.length){
			nrEqual = 0d;
			for(i = 0d; i < data1.length; i = i + 1d){
				if(data1[(int)(i)] == data2[(int)(i)]){
					nrEqual = nrEqual + 1d;
				}
			}
			if(nrEqual == data1.length){
				equal = true;
			}
		}else{
			equal = false;
		}

		return equal;
	}

	public static boolean numberArraysEqual(double [] data1, double [] data2){
		boolean equal;
		double nrEqual, i;

		equal = false;
		if(data1.length == data2.length){
			nrEqual = 0d;
			for(i = 0d; i < data1.length; i = i + 1d){
				if(data1[(int)(i)] == data2[(int)(i)]){
					nrEqual = nrEqual + 1d;
				}
			}
			if(nrEqual == data1.length){
				equal = true;
			}
		}else{
			equal = false;
		}

		return equal;
	}

	public static char [] substring(char [] string, double from, double to){
		char [] n;
		double i;

		n = new char [(int)(to - from)];

		for(i = from; i < to; i = i + 1d){
			n[(int)(i - from)] = string[(int)(i)];
		}

		return n;
	}

	public static char [] appendString(char [] string, char [] s){
		char [] newString;
		double i;

		newString = new char [(int)(string.length + s.length)];

		for(i = 0d; i < string.length; i = i + 1d){
			newString[(int)(i)] = string[(int)(i)];
		}

		for(i = 0d; i < s.length; i = i + 1d){
			newString[(int)(string.length + i)] = s[(int)(i)];
		}

		delete(string);

		return newString;
	}

	public static char [] appendCharacter(char [] string, char c){
		char [] newString;
		double i;

		newString = new char [(int)(string.length + 1d)];

		for(i = 0d; i < string.length; i = i + 1d){
			newString[(int)(i)] = string[(int)(i)];
		}

		newString[(int)(string.length)] = c;

		delete(string);

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
		for(i = 0d; i < toSplit.length; i = i + 1d){
			c = toSplit[(int)(i)];
			if(c == splitBy){
				n = new StringReference();
				n.string = next;
				splitt = addString(splitt, n);
				next = new char [0];
			}else{
				next = appendCharacter(next, c);
			}
		}

		if(next.length > 0d){
			n = new StringReference();
			n.string = next;
			splitt = addString(splitt, n);
		}

		return splitt;
	}

static public class BooleanReference{
	public boolean booleanValue;
}
static public class DoubleReference{
	public double doubleValue;
}
static public class StringReference{
	public char [] string;
}
static public class DecimalListRef{
	public double [] list;
}
static public class StringListRef{
	public StringReference [] list;
}
	public static double [] addDecimal(double [] list, double a){
		double [] newlist;
		double i;

		newlist = new double [(int)(list.length + 1d)];
		for(i = 0d; i < list.length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.length)] = a;
		
		delete(list);
		
		return newlist;
	}

	public static void addDecimalRef(DecimalListRef list, double i){

		list.list = addDecimal(list.list, i);
	}

	public static double [] removeDecimal(double [] list, double n){
		double [] newlist;
		double i;

		newlist = new double [(int)(list.length - 1d)];

		for(i = 0d; i < list.length; i = i + 1d){
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

		newlist = new StringReference [(int)(list.length + 1d)];

		for(i = 0d; i < list.length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.length)] = a;
		
		delete(list);
		
		return newlist;
	}

	public static void addStringRef(StringListRef list, StringReference i){

		list.list = addString(list.list, i);
	}

	public static StringReference [] removeString(StringReference [] list, double n){
		StringReference [] newlist;
		double i;

		newlist = new StringReference [(int)(list.length - 1d)];

		for(i = 0d; i < list.length; i = i + 1d){
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

  public static void delete(Object object){
    // Java has garbage collection.
  }


	public static void main(String [] args){
		System.out.println(test());
	}
}
