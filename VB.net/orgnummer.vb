Imports System
Imports System.Math

Public Structure StringToDecimalResult
	Public result As Double
	Public feilmelding As Char ()
	Public success As Boolean
End Structure

Public Structure BooleanReference
	Public booleanValue As Boolean
End Structure

Public Structure DoubleReference
	Public doubleValue As Double
End Structure

Public Structure StringReference
	Public stringxx As Char ()
End Structure

Public Structure DecimalListRef
	Public list As Double ()
End Structure

Public Structure StringListRef
	Public list As StringReference ()
End Structure

Module orgnummer
	Public Function erGyldigOrgNummer(ByRef orgnummer As Char ()) As Boolean
		Dim gyldig As Boolean
		Dim d1, d2, d3, d4, d5, d6, d7, d8, d9 As Double
		Dim sum, rest, kontrollsiffer As Double
		Dim failures As DoubleReference

		If orgnummer.Length = 9
			failures = New DoubleReference()
			failures.doubleValue = 0
			d1 = charToNumber(orgnummer(0), failures)
			d2 = charToNumber(orgnummer(1), failures)
			d3 = charToNumber(orgnummer(2), failures)
			d4 = charToNumber(orgnummer(3), failures)
			d5 = charToNumber(orgnummer(4), failures)
			d6 = charToNumber(orgnummer(5), failures)
			d7 = charToNumber(orgnummer(6), failures)
			d8 = charToNumber(orgnummer(7), failures)
			d9 = charToNumber(orgnummer(8), failures)
			If failures.doubleValue = 0
				sum = d1*3 + d2*2 + d3*7 + d4*6 + d5*5 + d6*4 + d7*3 + d8*2
				rest = sum Mod 11
				kontrollsiffer = 11 - rest
				If rest <> 1 And kontrollsiffer = d9
					gyldig = true
				Else
					gyldig = false
				End If
			Else
				gyldig = false
			End If
		Else
			gyldig = false
		End If

		Return gyldig
	End Function


	Public Function charToNumber(c As Char, ByRef failures As DoubleReference) As Double
		Dim val As Double

		val = getDecimalFromSingleDecimalDigit(c, 10)

		If val > 0 And val <= 9
		Else
			failures.doubleValue = failures.doubleValue + 1
		End If

		Return val
	End Function


	Public Function test() As Double
		Dim failures As Double
		Dim gyldig As Boolean

		failures = 0

		gyldig = erGyldigOrgNummer("913965639".ToCharArray())

		If Not gyldig
			failures = failures + 1
		End If

		gyldig = erGyldigOrgNummer("913965638".ToCharArray())

		If gyldig
			failures = failures + 1
		End If

		Return failures
	End Function


	Public Function decimalToString(decimalx As Double) As Char ()

		Return numberToString(decimalx, 10)
	End Function


	Public Function numberToString(decimalx As Double, base As Double) As Char ()
		Dim stringxx As Char ()
		Dim digits As Double
		Dim digitPosition As Double
		Dim hasPrintedPoint As Boolean
		Dim i, d As Double

		stringxx = New Char (0 - 1){}

		digits = getDigits(base)

		' Find digitPosition:
		digitPosition = getDigitPosition(decimalx, base)

		decimalx = Round(decimalx*base ^ (-digitPosition  + digits - 1))

		hasPrintedPoint = false

		' Print leading zeros.
		If digitPosition < 0
			stringxx = appendCharacter(stringxx, "0"C)
			stringxx = appendCharacter(stringxx, "."C)
			hasPrintedPoint = true
			i = 0
			While i < -digitPosition  - 1
				stringxx = appendCharacter(stringxx, "0"C)
				i = i + 1
			End While
		End If

		' Print number.
		i = 0
		While i < digits
			d = Floor(decimalx/base ^ (digits - i - 1))
			If Not hasPrintedPoint And digitPosition - i + 1 = 0
				If decimalx <> 0
					stringxx = appendCharacter(stringxx, "."C)
				End If
				hasPrintedPoint = true
			End If
			If decimalx = 0 And hasPrintedPoint
			Else
				stringxx = appendCharacter(stringxx, getSingleDigitFromNumber(d, base))
			End If
			decimalx = decimalx - d*base ^ (digits - i - 1)
			i = i + 1
		End While

		' Print trailing zeros.
		i = 0
		While i < digitPosition - digits + 1
			stringxx = appendCharacter(stringxx, "0"C)
			i = i + 1
		End While

		' Done
		Return stringxx
	End Function


	Public Function getDigits(base As Double) As Double
		Dim t As Double

		t = 10 ^ 15
		Return Floor(Log10(t)/Log10(base))
	End Function


	Public Function getDigitPosition(decimalx As Double, base As Double) As Double
		Dim power As Double
		Dim t As Double

		power = Ceiling(Log10(decimalx)/Log10(base))

		t = decimalx*base ^ (-power )
		If t < base And t >= 1
		ElseIf t >= base
			power = power + 1
		ElseIf t < 1
			power = power - 1
		End If


		Return power
	End Function


	Public Function getSingleDigitFromNumber(c As Double, base As Double) As Char
		Dim numberTable As Char ()
		Dim retc As Char

		numberTable = getNumberTable()

		If c > base - 1
			retc = "?"C
		Else
			retc = numberTable(c)
		End If

		Return retc
	End Function


	Public Function getNumberTable() As Char ()
		Dim numberTable As Char ()

		numberTable = New Char (16 - 1){}
		numberTable(0) = "0"C
		numberTable(1) = "1"C
		numberTable(2) = "2"C
		numberTable(3) = "3"C
		numberTable(4) = "4"C
		numberTable(5) = "5"C
		numberTable(6) = "6"C
		numberTable(7) = "7"C
		numberTable(8) = "8"C
		numberTable(9) = "9"C
		numberTable(10) = "A"C
		numberTable(11) = "B"C
		numberTable(12) = "C"C
		numberTable(13) = "D"C
		numberTable(14) = "E"C
		numberTable(15) = "F"C
		Return numberTable
	End Function


	Public Function stringToDecimal(ByRef stringxx As Char ()) As StringToDecimalResult

		Return stringToDecimalForBase(stringxx, 10)
	End Function


	Public Function stringToDecimalForBase(ByRef stringxx As Char (), base As Double) As StringToDecimalResult
		Dim stringToDecimalResult As StringToDecimalResult
		Dim c As Char
		Dim i, j As Double
		Dim isPositive As Boolean
		Dim beforeDecimalPoint As Double
		Dim afterDecimalPoint As Double
		Dim n As Double
		Dim validCharacters As Double
		Dim d As Double

		stringToDecimalResult = New StringToDecimalResult()
		stringToDecimalResult.success = true
		i = 0
		isPositive = true
		beforeDecimalPoint = 0
		afterDecimalPoint = 0
		n = 0
		validCharacters = 0

		If base >= 2 And base <= 16
			j = 0
			While j < stringxx.Length
				c = stringxx(j)
				If isNumber(c, base) Or c = "."C Or c = "-"C
					validCharacters = validCharacters + 1
				End If
				j = j + 1
			End While
			If validCharacters = stringxx.Length
				If stringxx.Length > 0
					c = stringxx(i)
					If c = "-"C
						isPositive = false
						i = i + 1
					End If
					If i < stringxx.Length
						c = stringxx(i)
						If isNumber(c, base)
							
							While isNumber(c, base) And (i < stringxx.Length)
								beforeDecimalPoint = beforeDecimalPoint + 1
								i = i + 1
								If i < stringxx.Length
									c = stringxx(i)
								End If
							End While
							If i < stringxx.Length
								c = stringxx(i)
								If c = "."C
									i = i + 1
									If i < stringxx.Length
										c = stringxx(i)
										
										While isNumber(c, base) And (i < stringxx.Length)
											afterDecimalPoint = afterDecimalPoint + 1
											i = i + 1
											If i < stringxx.Length
												c = stringxx(i)
											End If
										End While
									Else
										stringToDecimalResult.success = false
										stringToDecimalResult.feilmelding = "Number must have digits after the decimal point.".ToCharArray()
									End If
								End If
							End If
						Else
							stringToDecimalResult.success = false
							stringToDecimalResult.feilmelding = "Number must start with digits (for negative numbers, after the optional negative sign).".ToCharArray()
						End If
					End If
					If stringToDecimalResult.success <> false
						i = 0
						If Not isPositive
							i = 1
						End If
						j = 0
						While j < beforeDecimalPoint
							c = stringxx(i)
							i = i + 1
							d = getDecimalFromSingleDecimalDigit(c, base)
							n = n + d*base ^ (beforeDecimalPoint - j - 1)
							j = j + 1
						End While
						If afterDecimalPoint > 0
							i = i + 1
							j = 0
							While j < afterDecimalPoint
								c = stringxx(i)
								i = i + 1
								d = getDecimalFromSingleDecimalDigit(c, base)
								n = n + d*base ^ (0 - j - 1)
								j = j + 1
							End While
						End If
						If Not isPositive
							n = -n 
						End If
						stringToDecimalResult.result = n
						stringToDecimalResult.success = true
					End If
				Else
					stringToDecimalResult.success = false
					stringToDecimalResult.feilmelding = "String has no content.".ToCharArray()
				End If
			Else
				stringToDecimalResult.success = false
				stringToDecimalResult.feilmelding = "String contains invalid character.".ToCharArray()
			End If
		Else
			stringToDecimalResult.success = false
			stringToDecimalResult.feilmelding = "Base must be from 2 to 16.".ToCharArray()
		End If

		Return stringToDecimalResult
	End Function


	Public Function getDecimalFromSingleDecimalDigit(c As Char, base As Double) As Double
		Dim numberTable As Char ()
		Dim i As Double
		Dim position As Double

		numberTable = getNumberTable()
		position = 0

		i = 0
		While i < base
			If numberTable(i) = c
				position = i
			End If
			i = i + 1
		End While

		Return position
	End Function


	Public Function isNumber(c As Char, base As Double) As Boolean
		Dim numberTable As Char ()
		Dim i As Double
		Dim found As Boolean

		numberTable = getNumberTable()
		found = false

		i = 0
		While i < base
			If numberTable(i) = c
				found = true
			End If
			i = i + 1
		End While

		Return found
	End Function


	Public Sub assertFalse(b As Boolean, ByRef failures As DoubleReference)

		If b
			failures.doubleValue = failures.doubleValue + 1
		End If
	End Sub


	Public Sub assertTrue(b As Boolean, ByRef failures As DoubleReference)

		If Not b
			failures.doubleValue = failures.doubleValue + 1
		End If
	End Sub


	Public Sub assertEquals(a As Double, b As Double, ByRef failures As DoubleReference)

		If a <> b
			failures.doubleValue = failures.doubleValue + 1
		End If
	End Sub


	Public Sub assertStringEquals(ByRef a As Char (), ByRef b As Char (), ByRef failures As DoubleReference)

		If Not stringsEqual(a, b)
			failures.doubleValue = failures.doubleValue + 1
		End If
	End Sub


	Public Function stringToNumberArray(ByRef stringxx As Char ()) As Double ()
		Dim i As Double
		Dim arrayx As Double ()

		arrayx = New Double (stringxx.Length - 1){}

		i = 0
		While i < stringxx.Length
			arrayx(i) = Convert.ToByte(stringxx(i))
			i = i + 1
		End While
		Return arrayx
	End Function


	Public Function numberArrayToString(ByRef arrayx As Double ()) As Char ()
		Dim i As Double
		Dim stringxx As Char ()

		stringxx = New Char (arrayx.Length - 1){}

		i = 0
		While i < arrayx.Length
			stringxx(i) = Convert.ToChar(Convert.ToInt64(arrayx(i)))
			i = i + 1
		End While
		Return stringxx
	End Function


	Public Function stringsEqual(ByRef data1 As Char (), ByRef data2 As Char ()) As Boolean
		Dim equal As Boolean
		Dim nrEqual, i As Double

		equal = false
		If data1.Length = data2.Length
			nrEqual = 0
			i = 0
			While i < data1.Length
				If data1(i) = data2(i)
					nrEqual = nrEqual + 1
				End If
				i = i + 1
			End While
			If nrEqual = data1.Length
				equal = true
			End If
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Function numberArraysEqual(ByRef data1 As Double (), ByRef data2 As Double ()) As Boolean
		Dim equal As Boolean
		Dim nrEqual, i As Double

		equal = false
		If data1.Length = data2.Length
			nrEqual = 0
			i = 0
			While i < data1.Length
				If data1(i) = data2(i)
					nrEqual = nrEqual + 1
				End If
				i = i + 1
			End While
			If nrEqual = data1.Length
				equal = true
			End If
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Function substring(ByRef stringxx As Char (), from As Double, tox As Double) As Char ()
		Dim n As Char ()
		Dim i As Double

		n = New Char (tox - from - 1){}

		i = from
		While i < tox
			n(i - from) = stringxx(i)
			i = i + 1
		End While

		Return n
	End Function


	Public Function appendString(ByRef stringxx As Char (), ByRef s As Char ()) As Char ()
		Dim newString As Char ()
		Dim i As Double

		newString = New Char (stringxx.Length + s.Length - 1){}

		i = 0
		While i < stringxx.Length
			newString(i) = stringxx(i)
			i = i + 1
		End While

		i = 0
		While i < s.Length
			newString(stringxx.Length + i) = s(i)
			i = i + 1
		End While

		Erase stringxx 

		Return newString
	End Function


	Public Function appendCharacter(ByRef stringxx As Char (), c As Char) As Char ()
		Dim newString As Char ()
		Dim i As Double

		newString = New Char (stringxx.Length + 1 - 1){}

		i = 0
		While i < stringxx.Length
			newString(i) = stringxx(i)
			i = i + 1
		End While

		newString(stringxx.Length) = c

		Erase stringxx 

		Return newString
	End Function


	Public Function split(ByRef toSplit As Char (), splitBy As Char) As StringReference ()
		Dim splitt As StringReference ()
		Dim nextxx As Char ()
		Dim i As Double
		Dim c As Char
		Dim n As StringReference

		splitt = New StringReference (0 - 1){}

		nextxx = New Char (0 - 1){}
		i = 0
		While i < toSplit.Length
			c = toSplit(i)
			If c = splitBy
				n = New StringReference()
				n.stringxx = nextxx
				splitt = addString(splitt, n)
				nextxx = New Char (0 - 1){}
			Else
				nextxx = appendCharacter(nextxx, c)
			End If
			i = i + 1
		End While

		If nextxx.Length > 0
			n = New StringReference()
			n.stringxx = nextxx
			splitt = addString(splitt, n)
		End If

		Return splitt
	End Function


	Public Function addDecimal(ByRef list As Double (), a As Double) As Double ()
		Dim newlist As Double ()
		Dim i As Double

		newlist = New Double (list.Length + 1 - 1){}
		i = 0
		While i < list.Length
			newlist(i) = list(i)
			i = i + 1
		End While
		newlist(list.Length) = a
		
		Erase list 
		
		Return newlist
	End Function


	Public Sub addDecimalRef(ByRef list As DecimalListRef, i As Double)

		list.list = addDecimal(list.list, i)
	End Sub


	Public Function removeDecimal(ByRef list As Double (), n As Double) As Double ()
		Dim newlist As Double ()
		Dim i As Double

		newlist = New Double (list.Length - 1 - 1){}

		i = 0
		While i < list.Length
			If i < n
				newlist(i) = list(i)
			End If
			If i > n
				newlist(i - 1) = list(i)
			End If
			i = i + 1
		End While
		
		Erase list 
		
		Return newlist
	End Function


	Public Function getDecimalRef(ByRef list As DecimalListRef, i As Double) As Double

		Return list.list(i)
	End Function


	Public Sub removeDecimalRef(ByRef list As DecimalListRef, i As Double)

		list.list = removeDecimal(list.list, i)
	End Sub


	Public Function addString(ByRef list As StringReference (), ByRef a As StringReference) As StringReference ()
		Dim newlist As StringReference ()
		Dim i As Double

		newlist = New StringReference (list.Length + 1 - 1){}

		i = 0
		While i < list.Length
			newlist(i) = list(i)
			i = i + 1
		End While
		newlist(list.Length) = a
		
		Erase list 
		
		Return newlist
	End Function


	Public Sub addStringRef(ByRef list As StringListRef, ByRef i As StringReference)

		list.list = addString(list.list, i)
	End Sub


	Public Function removeString(ByRef list As StringReference (), n As Double) As StringReference ()
		Dim newlist As StringReference ()
		Dim i As Double

		newlist = New StringReference (list.Length - 1 - 1){}

		i = 0
		While i < list.Length
			If i < n
				newlist(i) = list(i)
			End If
			If i > n
				newlist(i - 1) = list(i)
			End If
			i = i + 1
		End While
		
		Erase list 
		
		Return newlist
	End Function


	Public Function getStringRef(ByRef list As StringListRef, i As Double) As StringReference

		Return list.list(i)
	End Function


	Public Sub removeStringRef(ByRef list As StringListRef, i As Double)

		list.list = removeString(list.list, i)
	End Sub


End Module


Module orgnummerTestBundle
	Sub Main(args As String())
		Console.WriteLine(test())
	End Sub
End Module
