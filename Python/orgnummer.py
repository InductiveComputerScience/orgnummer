from math import *
def erGyldigOrgNummer(orgnummer):
  if len(orgnummer) == 9.0:
    failures = DoubleReference()
    failures.doubleValue = 0.0
    d1 = charToNumber(orgnummer[int(0.0)], failures)
    d2 = charToNumber(orgnummer[int(1.0)], failures)
    d3 = charToNumber(orgnummer[int(2.0)], failures)
    d4 = charToNumber(orgnummer[int(3.0)], failures)
    d5 = charToNumber(orgnummer[int(4.0)], failures)
    d6 = charToNumber(orgnummer[int(5.0)], failures)
    d7 = charToNumber(orgnummer[int(6.0)], failures)
    d8 = charToNumber(orgnummer[int(7.0)], failures)
    d9 = charToNumber(orgnummer[int(8.0)], failures)
    if failures.doubleValue == 0.0:
      sum = d1*3.0 + d2*2.0 + d3*7.0 + d4*6.0 + d5*5.0 + d6*4.0 + d7*3.0 + d8*2.0
      rest = sum % 11.0
      kontrollsiffer = 11.0 - rest
      if rest != 1.0 and kontrollsiffer == d9:
        gyldig = True
      else:
        gyldig = False
    else:
      gyldig = False
  else:
    gyldig = False

  return gyldig

def charToNumber(c, failures):
  val = getDecimalFromSingleDecimalDigit(c, 10.0)

  if val > 0.0 and val <= 9.0:
    pass
  else:
    failures.doubleValue = failures.doubleValue + 1.0

  return val

def test():
  failures = 0.0

  gyldig = erGyldigOrgNummer("913965639")

  if  not gyldig :
    failures = failures + 1.0

  gyldig = erGyldigOrgNummer("913965638")

  if gyldig:
    failures = failures + 1.0

  return failures

class StringToDecimalResult:
  result = None
  feilmelding = None
  success = None

def decimalToString(decimal):
  return numberToString(decimal, 10.0)

def numberToString(decimal, base):
  string =  [None]*int(0.0)

  digits = getDigits(base)

  # Find digitPosition:
  digitPosition = getDigitPosition(decimal, base)

  decimal = round(decimal*base**( -digitPosition  + digits - 1.0))

  hasPrintedPoint = False

  # Print leading zeros.
  if digitPosition < 0.0:
    string = appendCharacter(string, '0')
    string = appendCharacter(string, '.')
    hasPrintedPoint = True
    i = 0.0
    while i <  -digitPosition  - 1.0:
      string = appendCharacter(string, '0')
      i = i + 1.0
    

  # Print number.
  i = 0.0
  while i < digits:
    d = floor(decimal/base**(digits - i - 1.0))
    if  not hasPrintedPoint  and digitPosition - i + 1.0 == 0.0:
      if decimal != 0.0:
        string = appendCharacter(string, '.')
      hasPrintedPoint = True
    if decimal == 0.0 and hasPrintedPoint:
      pass
    else:
      string = appendCharacter(string, getSingleDigitFromNumber(d, base))
    decimal = decimal - d*base**(digits - i - 1.0)
    i = i + 1.0
  

  # Print trailing zeros.
  i = 0.0
  while i < digitPosition - digits + 1.0:
    string = appendCharacter(string, '0')
    i = i + 1.0
  

  # Done
  return string

def getDigits(base):
  t = 10.0**15.0
  return floor(log10(t)/log10(base))

def getDigitPosition(decimal, base):
  power = ceil(log10(decimal)/log10(base))

  t = decimal*base**( -power )
  if t < base and t >= 1.0:
    pass
  elif t >= base:
    power = power + 1.0
  elif t < 1.0:
    power = power - 1.0
  return power

def getSingleDigitFromNumber(c, base):
  numberTable = getNumberTable()

  if c > base - 1.0:
    retc = '?'
  else:
    retc = numberTable[int(c)]

  return retc

def getNumberTable():
  numberTable =  [None]*int(16.0)
  numberTable[int(0.0)] = '0'
  numberTable[int(1.0)] = '1'
  numberTable[int(2.0)] = '2'
  numberTable[int(3.0)] = '3'
  numberTable[int(4.0)] = '4'
  numberTable[int(5.0)] = '5'
  numberTable[int(6.0)] = '6'
  numberTable[int(7.0)] = '7'
  numberTable[int(8.0)] = '8'
  numberTable[int(9.0)] = '9'
  numberTable[int(10.0)] = 'A'
  numberTable[int(11.0)] = 'B'
  numberTable[int(12.0)] = 'C'
  numberTable[int(13.0)] = 'D'
  numberTable[int(14.0)] = 'E'
  numberTable[int(15.0)] = 'F'
  return numberTable

def stringToDecimal(string):
  return stringToDecimalForBase(string, 10.0)

def stringToDecimalForBase(string, base):
  stringToDecimalResult = StringToDecimalResult()
  stringToDecimalResult.success = True
  i = 0.0
  isPositive = True
  beforeDecimalPoint = 0.0
  afterDecimalPoint = 0.0
  n = 0.0
  validCharacters = 0.0

  if base >= 2.0 and base <= 16.0:
    j = 0.0
    while j < len(string):
      c = string[int(j)]
      if isNumber(c, base) or c == '.' or c == '-':
        validCharacters = validCharacters + 1.0
      j = j + 1.0
    
    if validCharacters == len(string):
      if len(string) > 0.0:
        c = string[int(i)]
        if c == '-':
          isPositive = False
          i = i + 1.0
        if i < len(string):
          c = string[int(i)]
          if isNumber(c, base):
            while isNumber(c, base) and (i < len(string)):
              beforeDecimalPoint = beforeDecimalPoint + 1.0
              i = i + 1.0
              if i < len(string):
                c = string[int(i)]
            
            if i < len(string):
              c = string[int(i)]
              if c == '.':
                i = i + 1.0
                if i < len(string):
                  c = string[int(i)]
                  while isNumber(c, base) and (i < len(string)):
                    afterDecimalPoint = afterDecimalPoint + 1.0
                    i = i + 1.0
                    if i < len(string):
                      c = string[int(i)]
                  
                else:
                  stringToDecimalResult.success = False
                  stringToDecimalResult.feilmelding = "Number must have digits after the decimal point."
          else:
            stringToDecimalResult.success = False
            stringToDecimalResult.feilmelding = "Number must start with digits (for negative numbers, after the optional negative sign)."
        if stringToDecimalResult.success != False:
          i = 0.0
          if  not isPositive :
            i = 1.0
          j = 0.0
          while j < beforeDecimalPoint:
            c = string[int(i)]
            i = i + 1.0
            d = getDecimalFromSingleDecimalDigit(c, base)
            n = n + d*base**(beforeDecimalPoint - j - 1.0)
            j = j + 1.0
          
          if afterDecimalPoint > 0.0:
            i = i + 1.0
            j = 0.0
            while j < afterDecimalPoint:
              c = string[int(i)]
              i = i + 1.0
              d = getDecimalFromSingleDecimalDigit(c, base)
              n = n + d*base**(0.0 - j - 1.0)
              j = j + 1.0
            
          if  not isPositive :
            n =  -n 
          stringToDecimalResult.result = n
          stringToDecimalResult.success = True
      else:
        stringToDecimalResult.success = False
        stringToDecimalResult.feilmelding = "String has no content."
    else:
      stringToDecimalResult.success = False
      stringToDecimalResult.feilmelding = "String contains invalid character."
  else:
    stringToDecimalResult.success = False
    stringToDecimalResult.feilmelding = "Base must be from 2 to 16."

  return stringToDecimalResult

def getDecimalFromSingleDecimalDigit(c, base):
  numberTable = getNumberTable()
  position = 0.0

  i = 0.0
  while i < base:
    if numberTable[int(i)] == c:
      position = i
    i = i + 1.0
  

  return position

def isNumber(c, base):
  numberTable = getNumberTable()
  found = False

  i = 0.0
  while i < base:
    if numberTable[int(i)] == c:
      found = True
    i = i + 1.0
  

  return found

def assertFalse(b, failures):
  if b:
    failures.doubleValue = failures.doubleValue + 1.0

def assertTrue(b, failures):
  if  not b :
    failures.doubleValue = failures.doubleValue + 1.0

def assertEquals(a, b, failures):
  if a != b:
    failures.doubleValue = failures.doubleValue + 1.0

def assertStringEquals(a, b, failures):
  if  not stringsEqual(a, b) :
    failures.doubleValue = failures.doubleValue + 1.0

def stringToNumberArray(string):
  array =  [None]*int(len(string))

  i = 0.0
  while i < len(string):
    array[int(i)] = ord(string[int(i)])
    i = i + 1.0
  
  return array

def numberArrayToString(array):
  string =  [None]*int(len(array))

  i = 0.0
  while i < len(array):
    string[int(i)] = chr(int(array[int(i)]))
    i = i + 1.0
  
  return string

def stringsEqual(data1, data2):
  equal = False
  if len(data1) == len(data2):
    nrEqual = 0.0
    i = 0.0
    while i < len(data1):
      if data1[int(i)] == data2[int(i)]:
        nrEqual = nrEqual + 1.0
      i = i + 1.0
    
    if nrEqual == len(data1):
      equal = True
  else:
    equal = False

  return equal

def numberArraysEqual(data1, data2):
  equal = False
  if len(data1) == len(data2):
    nrEqual = 0.0
    i = 0.0
    while i < len(data1):
      if data1[int(i)] == data2[int(i)]:
        nrEqual = nrEqual + 1.0
      i = i + 1.0
    
    if nrEqual == len(data1):
      equal = True
  else:
    equal = False

  return equal

def substring(string, fromx, to):
  n =  [None]*int(to - fromx)

  i = fromx
  while i < to:
    n[int(i - fromx)] = string[int(i)]
    i = i + 1.0
  

  return n

def appendString(string, s):
  newString =  [None]*int(len(string) + len(s))

  i = 0.0
  while i < len(string):
    newString[int(i)] = string[int(i)]
    i = i + 1.0
  

  i = 0.0
  while i < len(s):
    newString[int(len(string) + i)] = s[int(i)]
    i = i + 1.0
  

  del(string)

  return newString

def appendCharacter(string, c):
  newString =  [None]*int(len(string) + 1.0)

  i = 0.0
  while i < len(string):
    newString[int(i)] = string[int(i)]
    i = i + 1.0
  

  newString[int(len(string))] = c

  del(string)

  return newString

def split(toSplit, splitBy):
  splitt =  [None]*int(0.0)

  next =  [None]*int(0.0)
  i = 0.0
  while i < len(toSplit):
    c = toSplit[int(i)]
    if c == splitBy:
      n = StringReference()
      n.string = next
      splitt = addString(splitt, n)
      next =  [None]*int(0.0)
    else:
      next = appendCharacter(next, c)
    i = i + 1.0
  

  if len(next) > 0.0:
    n = StringReference()
    n.string = next
    splitt = addString(splitt, n)

  return splitt

class BooleanReference:
  booleanValue = None

class DoubleReference:
  doubleValue = None

class StringReference:
  string = None

class DecimalListRef:
  list = None

class StringListRef:
  list = None

def addDecimal(list, a):
  newlist =  [None]*int(len(list) + 1.0)
  i = 0.0
  while i < len(list):
    newlist[int(i)] = list[int(i)]
    i = i + 1.0
  
  newlist[int(len(list))] = a
		
  del(list)
		
  return newlist

def addDecimalRef(list, i):
  list.list = addDecimal(list.list, i)

def removeDecimal(list, n):
  newlist =  [None]*int(len(list) - 1.0)

  i = 0.0
  while i < len(list):
    if i < n:
      newlist[int(i)] = list[int(i)]
    if i > n:
      newlist[int(i - 1.0)] = list[int(i)]
    i = i + 1.0
  
		
  del(list)
		
  return newlist

def getDecimalRef(list, i):
  return list.list[int(i)]

def removeDecimalRef(list, i):
  list.list = removeDecimal(list.list, i)

def addString(list, a):
  newlist =  [None]*int(len(list) + 1.0)

  i = 0.0
  while i < len(list):
    newlist[int(i)] = list[int(i)]
    i = i + 1.0
  
  newlist[int(len(list))] = a
		
  del(list)
		
  return newlist

def addStringRef(list, i):
  list.list = addString(list.list, i)

def removeString(list, n):
  newlist =  [None]*int(len(list) - 1.0)

  i = 0.0
  while i < len(list):
    if i < n:
      newlist[int(i)] = list[int(i)]
    if i > n:
      newlist[int(i - 1.0)] = list[int(i)]
    i = i + 1.0
  
		
  del(list)
		
  return newlist

def getStringRef(list, i):
  return list.list[int(i)]

def removeStringRef(list, i):
  list.list = removeString(list.list, i)



print(test())