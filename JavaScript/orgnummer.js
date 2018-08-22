function erGyldigOrgNummer(orgnummer){
  var gyldig;
  var d1, d2, d3, d4, d5, d6, d7, d8, d9;
  var sum, rest, kontrollsiffer;
  var failures;
  if(orgnummer.length == 9){
    failures = {};
    failures.doubleValue = 0;
    d1 = charToNumber(orgnummer[0], failures);
    d2 = charToNumber(orgnummer[1], failures);
    d3 = charToNumber(orgnummer[2], failures);
    d4 = charToNumber(orgnummer[3], failures);
    d5 = charToNumber(orgnummer[4], failures);
    d6 = charToNumber(orgnummer[5], failures);
    d7 = charToNumber(orgnummer[6], failures);
    d8 = charToNumber(orgnummer[7], failures);
    d9 = charToNumber(orgnummer[8], failures);
    if(failures.doubleValue == 0){
      sum = d1*3 + d2*2 + d3*7 + d4*6 + d5*5 + d6*4 + d7*3 + d8*2;
      rest = sum%11;
      kontrollsiffer = 11 - rest;
      if(rest != 1 && kontrollsiffer == d9){
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
function charToNumber(c, failures){
  var val;
  val = getDecimalFromSingleDecimalDigit(c, 10);

  if(val > 0 && val <= 9){
  }else{
    failures.doubleValue = failures.doubleValue + 1;
  }

  return val;
}
function test(){
  var failures;
  var gyldig;
  failures = 0;

  gyldig = erGyldigOrgNummer("913965639");

  if( !gyldig ){
    failures = failures + 1;
  }

  gyldig = erGyldigOrgNummer("913965638");

  if(gyldig){
    failures = failures + 1;
  }

  return failures;
}
function decimalToString(decimal){
  return numberToString(decimal, 10);
}
function numberToString(decimal, base){
  var string;
  var digits;
  var digitPosition;
  var hasPrintedPoint;
  var i, d;
  string = [];
  string.length = 0;

  digits = getDigits(base);

  /* Find digitPosition: */
  digitPosition = getDigitPosition(decimal, base);

  decimal = Math.round(decimal*Math.pow(base,  -digitPosition  + digits - 1));

  hasPrintedPoint = false;

  /* Print leading zeros. */
  if(digitPosition < 0){
    string = appendCharacter(string, '0');
    string = appendCharacter(string, '.');
    hasPrintedPoint = true;
    for(i = 0; i <  -digitPosition  - 1; i = i + 1){
      string = appendCharacter(string, '0');
    }
  }

  /* Print number. */
  for(i = 0; i < digits; i = i + 1){
    d = Math.floor(decimal/Math.pow(base, digits - i - 1));
    if( !hasPrintedPoint  && digitPosition - i + 1 == 0){
      if(decimal != 0){
        string = appendCharacter(string, '.');
      }
      hasPrintedPoint = true;
    }
    if(decimal == 0 && hasPrintedPoint){
    }else{
      string = appendCharacter(string, getSingleDigitFromNumber(d, base));
    }
    decimal = decimal - d*Math.pow(base, digits - i - 1);
  }

  /* Print trailing zeros. */
  for(i = 0; i < digitPosition - digits + 1; i = i + 1){
    string = appendCharacter(string, '0');
  }

  /* Done */
  return string;
}
function getDigits(base){
  var t;
  t = Math.pow(10, 15);
  return Math.floor(Math.log10(t)/Math.log10(base));
}
function getDigitPosition(decimal, base){
  var power;
  var t;
  power = Math.ceil(Math.log10(decimal)/Math.log10(base));

  t = decimal*Math.pow(base,  -power );
  if(t < base && t >= 1){
  }else if(t >= base){
    power = power + 1;
  }else if(t < 1){
    power = power - 1;
  }


  return power;
}
function getSingleDigitFromNumber(c, base){
  var numberTable;
  var retc;
  numberTable = getNumberTable();

  if(c > base - 1){
    retc = '?';
  }else{
    retc = numberTable[c];
  }

  return retc;
}
function getNumberTable(){
  var numberTable;
  numberTable = [];
  numberTable.length = 16;
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
function stringToDecimal(string){
  return stringToDecimalForBase(string, 10);
}
function stringToDecimalForBase(string, base){
  var stringToDecimalResult;
  var c;
  var i, j;
  var isPositive;
  var beforeDecimalPoint;
  var afterDecimalPoint;
  var n;
  var validCharacters;
  var d;
  stringToDecimalResult = {};
  stringToDecimalResult.success = true;
  i = 0;
  isPositive = true;
  beforeDecimalPoint = 0;
  afterDecimalPoint = 0;
  n = 0;
  validCharacters = 0;

  if(base >= 2 && base <= 16){
    for(j = 0; j < string.length; j = j + 1){
      c = string[j];
      if(isNumber(c, base) || c == '.' || c == '-'){
        validCharacters = validCharacters + 1;
      }
    }
    if(validCharacters == string.length){
      if(string.length > 0){
        c = string[i];
        if(c == '-'){
          isPositive = false;
          i = i + 1;
        }
        if(i < string.length){
          c = string[i];
          if(isNumber(c, base)){
            for(; isNumber(c, base) && (i < string.length); ){
              beforeDecimalPoint = beforeDecimalPoint + 1;
              i = i + 1;
              if(i < string.length){
                c = string[i];
              }
            }
            if(i < string.length){
              c = string[i];
              if(c == '.'){
                i = i + 1;
                if(i < string.length){
                  c = string[i];
                  for(; isNumber(c, base) && (i < string.length); ){
                    afterDecimalPoint = afterDecimalPoint + 1;
                    i = i + 1;
                    if(i < string.length){
                      c = string[i];
                    }
                  }
                }else{
                  stringToDecimalResult.success = false;
                  stringToDecimalResult.feilmelding = "Number must have digits after the decimal point.";
                }
              }
            }
          }else{
            stringToDecimalResult.success = false;
            stringToDecimalResult.feilmelding = "Number must start with digits (for negative numbers, after the optional negative sign).";
          }
        }
        if(stringToDecimalResult.success != false){
          i = 0;
          if( !isPositive ){
            i = 1;
          }
          for(j = 0; j < beforeDecimalPoint; j = j + 1){
            c = string[i];
            i = i + 1;
            d = getDecimalFromSingleDecimalDigit(c, base);
            n = n + d*Math.pow(base, beforeDecimalPoint - j - 1);
          }
          if(afterDecimalPoint > 0){
            i = i + 1;
            for(j = 0; j < afterDecimalPoint; j = j + 1){
              c = string[i];
              i = i + 1;
              d = getDecimalFromSingleDecimalDigit(c, base);
              n = n + d*Math.pow(base, 0 - j - 1);
            }
          }
          if( !isPositive ){
            n =  -n ;
          }
          stringToDecimalResult.result = n;
          stringToDecimalResult.success = true;
        }
      }else{
        stringToDecimalResult.success = false;
        stringToDecimalResult.feilmelding = "String has no content.";
      }
    }else{
      stringToDecimalResult.success = false;
      stringToDecimalResult.feilmelding = "String contains invalid character.";
    }
  }else{
    stringToDecimalResult.success = false;
    stringToDecimalResult.feilmelding = "Base must be from 2 to 16.";
  }

  return stringToDecimalResult;
}
function getDecimalFromSingleDecimalDigit(c, base){
  var numberTable;
  var i;
  var position;
  numberTable = getNumberTable();
  position = 0;

  for(i = 0; i < base; i = i + 1){
    if(numberTable[i] == c){
      position = i;
    }
  }

  return position;
}
function isNumber(c, base){
  var numberTable;
  var i;
  var found;
  numberTable = getNumberTable();
  found = false;

  for(i = 0; i < base; i = i + 1){
    if(numberTable[i] == c){
      found = true;
    }
  }

  return found;
}
function assertFalse(b, failures){
  if(b){
    failures.doubleValue = failures.doubleValue + 1;
  }
}
function assertTrue(b, failures){
  if( !b ){
    failures.doubleValue = failures.doubleValue + 1;
  }
}
function assertEquals(a, b, failures){
  if(a != b){
    failures.doubleValue = failures.doubleValue + 1;
  }
}
function assertStringEquals(a, b, failures){
  if( !stringsEqual(a, b) ){
    failures.doubleValue = failures.doubleValue + 1;
  }
}
function stringToNumberArray(string){
  var i;
  var array;
  array = [];
  array.length = string.length;

  for(i = 0; i < string.length; i = i + 1){
    array[i] = string[i].codePointAt(0);
  }
  return array;
}
function numberArrayToString(array){
  var i;
  var string;
  string = [];
  string.length = array.length;

  for(i = 0; i < array.length; i = i + 1){
    string[i] = String.fromCharCode(array[i]);
  }
  return string;
}
function stringsEqual(data1, data2){
  var equal;
  var nrEqual, i;
  equal = false;
  if(data1.length == data2.length){
    nrEqual = 0;
    for(i = 0; i < data1.length; i = i + 1){
      if(data1[i] == data2[i]){
        nrEqual = nrEqual + 1;
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
function numberArraysEqual(data1, data2){
  var equal;
  var nrEqual, i;
  equal = false;
  if(data1.length == data2.length){
    nrEqual = 0;
    for(i = 0; i < data1.length; i = i + 1){
      if(data1[i] == data2[i]){
        nrEqual = nrEqual + 1;
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
function substring(string, from, to){
  var n;
  var i;
  n = [];
  n.length = to - from;

  for(i = from; i < to; i = i + 1){
    n[i - from] = string[i];
  }

  return n;
}
function appendString(string, s){
  var newString;
  var i;
  newString = [];
  newString.length = string.length + s.length;

  for(i = 0; i < string.length; i = i + 1){
    newString[i] = string[i];
  }

  for(i = 0; i < s.length; i = i + 1){
    newString[string.length + i] = s[i];
  }

  delete(string);

  return newString;
}
function appendCharacter(string, c){
  var newString;
  var i;
  newString = [];
  newString.length = string.length + 1;

  for(i = 0; i < string.length; i = i + 1){
    newString[i] = string[i];
  }

  newString[string.length] = c;

  delete(string);

  return newString;
}
function split(toSplit, splitBy){
  var splitt;
  var next;
  var i;
  var c;
  var n;
  splitt = [];
  splitt.length = 0;

  next = [];
  next.length = 0;
  for(i = 0; i < toSplit.length; i = i + 1){
    c = toSplit[i];
    if(c == splitBy){
      n = {};
      n.string = next;
      splitt = addString(splitt, n);
      next = [];
      next.length = 0;
    }else{
      next = appendCharacter(next, c);
    }
  }

  if(next.length > 0){
    n = {};
    n.string = next;
    splitt = addString(splitt, n);
  }

  return splitt;
}
function addDecimal(list, a){
  var newlist;
  var i;
  newlist = [];
  newlist.length = list.length + 1;
  for(i = 0; i < list.length; i = i + 1){
    newlist[i] = list[i];
  }
  newlist[list.length] = a;
		
  delete(list);
		
  return newlist;
}
function addDecimalRef(list, i){
  list.list = addDecimal(list.list, i);
}
function removeDecimal(list, n){
  var newlist;
  var i;
  newlist = [];
  newlist.length = list.length - 1;

  for(i = 0; i < list.length; i = i + 1){
    if(i < n){
      newlist[i] = list[i];
    }
    if(i > n){
      newlist[i - 1] = list[i];
    }
  }
		
  delete(list);
		
  return newlist;
}
function getDecimalRef(list, i){
  return list.list[i];
}
function removeDecimalRef(list, i){
  list.list = removeDecimal(list.list, i);
}
function addString(list, a){
  var newlist;
  var i;
  newlist = [];
  newlist.length = list.length + 1;

  for(i = 0; i < list.length; i = i + 1){
    newlist[i] = list[i];
  }
  newlist[list.length] = a;
		
  delete(list);
		
  return newlist;
}
function addStringRef(list, i){
  list.list = addString(list.list, i);
}
function removeString(list, n){
  var newlist;
  var i;
  newlist = [];
  newlist.length = list.length - 1;

  for(i = 0; i < list.length; i = i + 1){
    if(i < n){
      newlist[i] = list[i];
    }
    if(i > n){
      newlist[i - 1] = list[i];
    }
  }
		
  delete(list);
		
  return newlist;
}
function getStringRef(list, i){
  return list.list[i];
}
function removeStringRef(list, i){
  list.list = removeString(list.list, i);
}


console.log(test());