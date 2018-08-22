#include <cmath>
#include <cstring>
#include <vector>

using namespace std;

#define toVector(s) (new vector<char> ((s), (s) + strlen(s)))

struct StringToDecimalResult;

struct BooleanReference;

struct DoubleReference;

struct StringReference;

struct DecimalListRef;

struct StringListRef;

struct StringToDecimalResult{
  double result;
  vector<char> *feilmelding;
  bool success;
};

struct BooleanReference{
  bool booleanValue;
};

struct DoubleReference{
  double doubleValue;
};

struct StringReference{
  vector<char> *string;
};

struct DecimalListRef{
  vector<double> *list;
};

struct StringListRef{
  vector<StringReference*> *list;
};

bool erGyldigOrgNummer(vector<char> *orgnummer);
double charToNumber(char c, DoubleReference *failures);

double test();

vector<char> *decimalToString(double decimal);
vector<char> *numberToString(double decimal, double base);
double getDigits(double base);
double getDigitPosition(double decimal, double base);
char getSingleDigitFromNumber(double c, double base);
vector<char> *getNumberTable();

StringToDecimalResult *stringToDecimal(vector<char> *string);
StringToDecimalResult *stringToDecimalForBase(vector<char> *string, double base);
double getDecimalFromSingleDecimalDigit(char c, double base);
bool isNumber(char c, double base);

void assertFalse(bool b, DoubleReference *failures);
void assertTrue(bool b, DoubleReference *failures);
void assertEquals(double a, double b, DoubleReference *failures);
void assertStringEquals(vector<char> *a, vector<char> *b, DoubleReference *failures);

vector<double> *stringToNumberArray(vector<char> *string);
vector<char> *numberArrayToString(vector<double> *array);
bool stringsEqual(vector<char> *data1, vector<char> *data2);
bool numberArraysEqual(vector<double> *data1, vector<double> *data2);

vector<char> *substring(vector<char> *string, double from, double to);
vector<char> *appendString(vector<char> *string, vector<char> *s);
vector<char> *appendCharacter(vector<char> *string, char c);
vector<StringReference*> *split(vector<char> *toSplit, char splitBy);


vector<double> *addDecimal(vector<double> *list, double a);
void addDecimalRef(DecimalListRef *list, double i);
vector<double> *removeDecimal(vector<double> *list, double n);
double getDecimalRef(DecimalListRef *list, double i);
void removeDecimalRef(DecimalListRef *list, double i);

vector<StringReference*> *addString(vector<StringReference*> *list, StringReference *a);
void addStringRef(StringListRef *list, StringReference *i);
vector<StringReference*> *removeString(vector<StringReference*> *list, double n);
StringReference *getStringRef(StringListRef *list, double i);
void removeStringRef(StringListRef *list, double i);

bool erGyldigOrgNummer(vector<char> *orgnummer){
  bool gyldig;
  double d1, d2, d3, d4, d5, d6, d7, d8, d9;
  double sum, rest, kontrollsiffer;
  DoubleReference *failures;

  if(orgnummer->size() == 9.0){
    failures = new DoubleReference();
    failures->doubleValue = 0.0;
    d1 = charToNumber((*orgnummer)[0], failures);
    d2 = charToNumber((*orgnummer)[1], failures);
    d3 = charToNumber((*orgnummer)[2], failures);
    d4 = charToNumber((*orgnummer)[3], failures);
    d5 = charToNumber((*orgnummer)[4], failures);
    d6 = charToNumber((*orgnummer)[5], failures);
    d7 = charToNumber((*orgnummer)[6], failures);
    d8 = charToNumber((*orgnummer)[7], failures);
    d9 = charToNumber((*orgnummer)[8], failures);
    if(failures->doubleValue == 0.0){
      sum = d1*3.0 + d2*2.0 + d3*7.0 + d4*6.0 + d5*5.0 + d6*4.0 + d7*3.0 + d8*2.0;
      rest = fmod(sum, 11.0);
      kontrollsiffer = 11.0 - rest;
      if(rest != 1.0 && kontrollsiffer == d9){
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
double charToNumber(char c, DoubleReference *failures){
  double val;

  val = getDecimalFromSingleDecimalDigit(c, 10.0);

  if(val > 0.0 && val <= 9.0){
  }else{
    failures->doubleValue = failures->doubleValue + 1.0;
  }

  return val;
}
double test(){
  double failures;
  bool gyldig;

  failures = 0.0;

  gyldig = erGyldigOrgNummer(toVector("913965639"));

  if( !gyldig ){
    failures = failures + 1.0;
  }

  gyldig = erGyldigOrgNummer(toVector("913965638"));

  if(gyldig){
    failures = failures + 1.0;
  }

  return failures;
}
vector<char> *decimalToString(double decimal){

  return numberToString(decimal, 10.0);
}
vector<char> *numberToString(double decimal, double base){
  vector<char> *string;
  double digits;
  double digitPosition;
  bool hasPrintedPoint;
  double i, d;

  string = new vector<char> (0.0);

  digits = getDigits(base);

  /* Find digitPosition: */
  digitPosition = getDigitPosition(decimal, base);

  decimal = round(decimal*pow(base,  -digitPosition  + digits - 1.0));

  hasPrintedPoint = false;

  /* Print leading zeros. */
  if(digitPosition < 0.0){
    string = appendCharacter(string, '0');
    string = appendCharacter(string, '.');
    hasPrintedPoint = true;
    for(i = 0.0; i <  -digitPosition  - 1.0; i = i + 1.0){
      string = appendCharacter(string, '0');
    }
  }

  /* Print number. */
  for(i = 0.0; i < digits; i = i + 1.0){
    d = floor(decimal/pow(base, digits - i - 1.0));
    if( !hasPrintedPoint  && digitPosition - i + 1.0 == 0.0){
      if(decimal != 0.0){
        string = appendCharacter(string, '.');
      }
      hasPrintedPoint = true;
    }
    if(decimal == 0.0 && hasPrintedPoint){
    }else{
      string = appendCharacter(string, getSingleDigitFromNumber(d, base));
    }
    decimal = decimal - d*pow(base, digits - i - 1.0);
  }

  /* Print trailing zeros. */
  for(i = 0.0; i < digitPosition - digits + 1.0; i = i + 1.0){
    string = appendCharacter(string, '0');
  }

  /* Done */
  return string;
}
double getDigits(double base){
  double t;

  t = pow(10.0, 15.0);
  return floor(log10(t)/log10(base));
}
double getDigitPosition(double decimal, double base){
  double power;
  double t;

  power = ceil(log10(decimal)/log10(base));

  t = decimal*pow(base,  -power );
  if(t < base && t >= 1.0){
  }else if(t >= base){
    power = power + 1.0;
  }else if(t < 1.0){
    power = power - 1.0;
  }


  return power;
}
char getSingleDigitFromNumber(double c, double base){
  vector<char> *numberTable;
  char retc;

  numberTable = getNumberTable();

  if(c > base - 1.0){
    retc = '?';
  }else{
    retc = (*numberTable)[(int)(c)];
  }

  return retc;
}
vector<char> *getNumberTable(){
  vector<char> *numberTable;

  numberTable = new vector<char> (16.0);
  (*numberTable)[0] = '0';
  (*numberTable)[1] = '1';
  (*numberTable)[2] = '2';
  (*numberTable)[3] = '3';
  (*numberTable)[4] = '4';
  (*numberTable)[5] = '5';
  (*numberTable)[6] = '6';
  (*numberTable)[7] = '7';
  (*numberTable)[8] = '8';
  (*numberTable)[9] = '9';
  (*numberTable)[10] = 'A';
  (*numberTable)[11] = 'B';
  (*numberTable)[12] = 'C';
  (*numberTable)[13] = 'D';
  (*numberTable)[14] = 'E';
  (*numberTable)[15] = 'F';
  return numberTable;
}
StringToDecimalResult *stringToDecimal(vector<char> *string){

  return stringToDecimalForBase(string, 10.0);
}
StringToDecimalResult *stringToDecimalForBase(vector<char> *string, double base){
  StringToDecimalResult *stringToDecimalResult;
  char c;
  double i, j;
  bool isPositive;
  double beforeDecimalPoint;
  double afterDecimalPoint;
  double n;
  double validCharacters;
  double d;

  stringToDecimalResult = new StringToDecimalResult();
  stringToDecimalResult->success = true;
  i = 0.0;
  isPositive = true;
  beforeDecimalPoint = 0.0;
  afterDecimalPoint = 0.0;
  n = 0.0;
  validCharacters = 0.0;

  if(base >= 2.0 && base <= 16.0){
    for(j = 0.0; j < string->size(); j = j + 1.0){
      c = (*string)[(int)(j)];
      if(isNumber(c, base) || c == '.' || c == '-'){
        validCharacters = validCharacters + 1.0;
      }
    }
    if(validCharacters == string->size()){
      if(string->size() > 0.0){
        c = (*string)[(int)(i)];
        if(c == '-'){
          isPositive = false;
          i = i + 1.0;
        }
        if(i < string->size()){
          c = (*string)[(int)(i)];
          if(isNumber(c, base)){
            for(; isNumber(c, base) && (i < string->size()); ){
              beforeDecimalPoint = beforeDecimalPoint + 1.0;
              i = i + 1.0;
              if(i < string->size()){
                c = (*string)[(int)(i)];
              }
            }
            if(i < string->size()){
              c = (*string)[(int)(i)];
              if(c == '.'){
                i = i + 1.0;
                if(i < string->size()){
                  c = (*string)[(int)(i)];
                  for(; isNumber(c, base) && (i < string->size()); ){
                    afterDecimalPoint = afterDecimalPoint + 1.0;
                    i = i + 1.0;
                    if(i < string->size()){
                      c = (*string)[(int)(i)];
                    }
                  }
                }else{
                  stringToDecimalResult->success = false;
                  stringToDecimalResult->feilmelding = toVector("Number must have digits after the decimal point.");
                }
              }
            }
          }else{
            stringToDecimalResult->success = false;
            stringToDecimalResult->feilmelding = toVector("Number must start with digits (for negative numbers, after the optional negative sign).");
          }
        }
        if(stringToDecimalResult->success != false){
          i = 0.0;
          if( !isPositive ){
            i = 1.0;
          }
          for(j = 0.0; j < beforeDecimalPoint; j = j + 1.0){
            c = (*string)[(int)(i)];
            i = i + 1.0;
            d = getDecimalFromSingleDecimalDigit(c, base);
            n = n + d*pow(base, beforeDecimalPoint - j - 1.0);
          }
          if(afterDecimalPoint > 0.0){
            i = i + 1.0;
            for(j = 0.0; j < afterDecimalPoint; j = j + 1.0){
              c = (*string)[(int)(i)];
              i = i + 1.0;
              d = getDecimalFromSingleDecimalDigit(c, base);
              n = n + d*pow(base, 0.0 - j - 1.0);
            }
          }
          if( !isPositive ){
            n =  -n ;
          }
          stringToDecimalResult->result = n;
          stringToDecimalResult->success = true;
        }
      }else{
        stringToDecimalResult->success = false;
        stringToDecimalResult->feilmelding = toVector("String has no content.");
      }
    }else{
      stringToDecimalResult->success = false;
      stringToDecimalResult->feilmelding = toVector("String contains invalid character.");
    }
  }else{
    stringToDecimalResult->success = false;
    stringToDecimalResult->feilmelding = toVector("Base must be from 2 to 16.");
  }

  return stringToDecimalResult;
}
double getDecimalFromSingleDecimalDigit(char c, double base){
  vector<char> *numberTable;
  double i;
  double position;

  numberTable = getNumberTable();
  position = 0.0;

  for(i = 0.0; i < base; i = i + 1.0){
    if((*numberTable)[(int)(i)] == c){
      position = i;
    }
  }

  return position;
}
bool isNumber(char c, double base){
  vector<char> *numberTable;
  double i;
  bool found;

  numberTable = getNumberTable();
  found = false;

  for(i = 0.0; i < base; i = i + 1.0){
    if((*numberTable)[(int)(i)] == c){
      found = true;
    }
  }

  return found;
}
void assertFalse(bool b, DoubleReference *failures){

  if(b){
    failures->doubleValue = failures->doubleValue + 1.0;
  }
}
void assertTrue(bool b, DoubleReference *failures){

  if( !b ){
    failures->doubleValue = failures->doubleValue + 1.0;
  }
}
void assertEquals(double a, double b, DoubleReference *failures){

  if(a != b){
    failures->doubleValue = failures->doubleValue + 1.0;
  }
}
void assertStringEquals(vector<char> *a, vector<char> *b, DoubleReference *failures){

  if( !stringsEqual(a, b) ){
    failures->doubleValue = failures->doubleValue + 1.0;
  }
}
vector<double> *stringToNumberArray(vector<char> *string){
  double i;
  vector<double> *array;

  array = new vector<double> (string->size());

  for(i = 0.0; i < string->size(); i = i + 1.0){
    (*array)[(int)(i)] = (*string)[(int)(i)];
  }
  return array;
}
vector<char> *numberArrayToString(vector<double> *array){
  double i;
  vector<char> *string;

  string = new vector<char> (array->size());

  for(i = 0.0; i < array->size(); i = i + 1.0){
    (*string)[(int)(i)] = (*array)[(int)(i)];
  }
  return string;
}
bool stringsEqual(vector<char> *data1, vector<char> *data2){
  bool equal;
  double nrEqual, i;

  equal = false;
  if(data1->size() == data2->size()){
    nrEqual = 0.0;
    for(i = 0.0; i < data1->size(); i = i + 1.0){
      if((*data1)[(int)(i)] == (*data2)[(int)(i)]){
        nrEqual = nrEqual + 1.0;
      }
    }
    if(nrEqual == data1->size()){
      equal = true;
    }
  }else{
    equal = false;
  }

  return equal;
}
bool numberArraysEqual(vector<double> *data1, vector<double> *data2){
  bool equal;
  double nrEqual, i;

  equal = false;
  if(data1->size() == data2->size()){
    nrEqual = 0.0;
    for(i = 0.0; i < data1->size(); i = i + 1.0){
      if((*data1)[(int)(i)] == (*data2)[(int)(i)]){
        nrEqual = nrEqual + 1.0;
      }
    }
    if(nrEqual == data1->size()){
      equal = true;
    }
  }else{
    equal = false;
  }

  return equal;
}
vector<char> *substring(vector<char> *string, double from, double to){
  vector<char> *n;
  double i;

  n = new vector<char> (to - from);

  for(i = from; i < to; i = i + 1.0){
    (*n)[(int)(i - from)] = (*string)[(int)(i)];
  }

  return n;
}
vector<char> *appendString(vector<char> *string, vector<char> *s){
  vector<char> *newString;
  double i;

  newString = new vector<char> (string->size() + s->size());

  for(i = 0.0; i < string->size(); i = i + 1.0){
    (*newString)[(int)(i)] = (*string)[(int)(i)];
  }

  for(i = 0.0; i < s->size(); i = i + 1.0){
    (*newString)[(int)(string->size() + i)] = (*s)[(int)(i)];
  }

  delete string;

  return newString;
}
vector<char> *appendCharacter(vector<char> *string, char c){
  vector<char> *newString;
  double i;

  newString = new vector<char> (string->size() + 1.0);

  for(i = 0.0; i < string->size(); i = i + 1.0){
    (*newString)[(int)(i)] = (*string)[(int)(i)];
  }

  (*newString)[(int)(string->size())] = c;

  delete string;

  return newString;
}
vector<StringReference*> *split(vector<char> *toSplit, char splitBy){
  vector<StringReference*> *splitt;
  vector<char> *next;
  double i;
  char c;
  StringReference *n;

  splitt = new vector<StringReference*> (0.0);

  next = new vector<char> (0.0);
  for(i = 0.0; i < toSplit->size(); i = i + 1.0){
    c = (*toSplit)[(int)(i)];
    if(c == splitBy){
      n = new StringReference();
      n->string = next;
      splitt = addString(splitt, n);
      next = new vector<char> (0.0);
    }else{
      next = appendCharacter(next, c);
    }
  }

  if(next->size() > 0.0){
    n = new StringReference();
    n->string = next;
    splitt = addString(splitt, n);
  }

  return splitt;
}
vector<double> *addDecimal(vector<double> *list, double a){
  vector<double> *newlist;
  double i;

  newlist = new vector<double> (list->size() + 1.0);
  for(i = 0.0; i < list->size(); i = i + 1.0){
    (*newlist)[(int)(i)] = (*list)[(int)(i)];
  }
  (*newlist)[(int)(list->size())] = a;
		
  delete list;
		
  return newlist;
}
void addDecimalRef(DecimalListRef *list, double i){

  list->list = addDecimal(list->list, i);
}
vector<double> *removeDecimal(vector<double> *list, double n){
  vector<double> *newlist;
  double i;

  newlist = new vector<double> (list->size() - 1.0);

  for(i = 0.0; i < list->size(); i = i + 1.0){
    if(i < n){
      (*newlist)[(int)(i)] = (*list)[(int)(i)];
    }
    if(i > n){
      (*newlist)[(int)(i - 1.0)] = (*list)[(int)(i)];
    }
  }
		
  delete list;
		
  return newlist;
}
double getDecimalRef(DecimalListRef *list, double i){

  return (*list->list)[(int)(i)];
}
void removeDecimalRef(DecimalListRef *list, double i){

  list->list = removeDecimal(list->list, i);
}
vector<StringReference*> *addString(vector<StringReference*> *list, StringReference *a){
  vector<StringReference*> *newlist;
  double i;

  newlist = new vector<StringReference*> (list->size() + 1.0);

  for(i = 0.0; i < list->size(); i = i + 1.0){
    (*newlist)[(int)(i)] = (*list)[(int)(i)];
  }
  (*newlist)[(int)(list->size())] = a;
		
  delete list;
		
  return newlist;
}
void addStringRef(StringListRef *list, StringReference *i){

  list->list = addString(list->list, i);
}
vector<StringReference*> *removeString(vector<StringReference*> *list, double n){
  vector<StringReference*> *newlist;
  double i;

  newlist = new vector<StringReference*> (list->size() - 1.0);

  for(i = 0.0; i < list->size(); i = i + 1.0){
    if(i < n){
      (*newlist)[(int)(i)] = (*list)[(int)(i)];
    }
    if(i > n){
      (*newlist)[(int)(i - 1.0)] = (*list)[(int)(i)];
    }
  }
		
  delete list;
		
  return newlist;
}
StringReference *getStringRef(StringListRef *list, double i){

  return (*list->list)[(int)(i)];
}
void removeStringRef(StringListRef *list, double i){

  list->list = removeString(list->list, i);
}


#include <iostream>

using namespace std;

int main(){
	cout << test();
	return 0;
}