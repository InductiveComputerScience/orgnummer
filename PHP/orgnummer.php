<?php
function erGyldigOrgNummer(&$orgnummer){
  if(count($orgnummer) == 9.0){
    $failures = new stdClass();
    $failures->doubleValue = 0.0;
    $d1 = charToNumber($orgnummer[0.0], $failures);
    $d2 = charToNumber($orgnummer[1.0], $failures);
    $d3 = charToNumber($orgnummer[2.0], $failures);
    $d4 = charToNumber($orgnummer[3.0], $failures);
    $d5 = charToNumber($orgnummer[4.0], $failures);
    $d6 = charToNumber($orgnummer[5.0], $failures);
    $d7 = charToNumber($orgnummer[6.0], $failures);
    $d8 = charToNumber($orgnummer[7.0], $failures);
    $d9 = charToNumber($orgnummer[8.0], $failures);
    if($failures->doubleValue == 0.0){
      $sum = $d1*3.0 + $d2*2.0 + $d3*7.0 + $d4*6.0 + $d5*5.0 + $d6*4.0 + $d7*3.0 + $d8*2.0;
      $rest = $sum%11.0;
      $kontrollsiffer = 11.0 - $rest;
      if($rest != 1.0 && $kontrollsiffer == $d9){
        $gyldig = true;
      }else{
        $gyldig = false;
      }
    }else{
      $gyldig = false;
    }
  }else{
    $gyldig = false;
  }

  return $gyldig;
}
function charToNumber($c, $failures){
  $val = getDecimalFromSingleDecimalDigit($c, 10.0);

  if($val > 0.0 && $val <= 9.0){
  }else{
    $failures->doubleValue = $failures->doubleValue + 1.0;
  }

  return $val;
}
function test(){
  $failures = 0.0;

  $gyldig = erGyldigOrgNummer(str_split("913965639"));

  if( !$gyldig ){
    $failures = $failures + 1.0;
  }

  $gyldig = erGyldigOrgNummer(str_split("913965638"));

  if($gyldig){
    $failures = $failures + 1.0;
  }

  return $failures;
}
function &decimalToString($decimal){
  return numberToString($decimal, 10.0);
}
function &numberToString($decimal, $base){
  $string = array_fill(0, 0.0, 0);

  $digits = getDigits($base);

  /* Find digitPosition: */
  $digitPosition = getDigitPosition($decimal, $base);

  $decimal = round($decimal*pow($base, -$digitPosition + $digits - 1.0));

  $hasPrintedPoint = false;

  /* Print leading zeros. */
  if($digitPosition < 0.0){
    $string = appendCharacter($string, "0");
    $string = appendCharacter($string, ".");
    $hasPrintedPoint = true;
    for($i = 0.0; $i < -$digitPosition - 1.0; $i = $i + 1.0){
      $string = appendCharacter($string, "0");
    }
  }

  /* Print number. */
  for($i = 0.0; $i < $digits; $i = $i + 1.0){
    $d = floor($decimal/pow($base, $digits - $i - 1.0));
    if( !$hasPrintedPoint  && $digitPosition - $i + 1.0 == 0.0){
      if($decimal != 0.0){
        $string = appendCharacter($string, ".");
      }
      $hasPrintedPoint = true;
    }
    if($decimal == 0.0 && $hasPrintedPoint){
    }else{
      $string = appendCharacter($string, getSingleDigitFromNumber($d, $base));
    }
    $decimal = $decimal - $d*pow($base, $digits - $i - 1.0);
  }

  /* Print trailing zeros. */
  for($i = 0.0; $i < $digitPosition - $digits + 1.0; $i = $i + 1.0){
    $string = appendCharacter($string, "0");
  }

  /* Done */
  return $string;
}
function getDigits($base){
  $t = pow(10.0, 15.0);
  return floor(log10($t)/log10($base));
}
function getDigitPosition($decimal, $base){
  $power = ceil(log10($decimal)/log10($base));

  $t = $decimal*pow($base, -$power);
  if($t < $base && $t >= 1.0){
  }else if($t >= $base){
    $power = $power + 1.0;
  }else if($t < 1.0){
    $power = $power - 1.0;
  }


  return $power;
}
function getSingleDigitFromNumber($c, $base){
  $numberTable = getNumberTable();

  if($c > $base - 1.0){
    $retc = "?";
  }else{
    $retc = $numberTable[$c];
  }

  return $retc;
}
function &getNumberTable(){
  $numberTable = array_fill(0, 16.0, 0);
  $numberTable[0.0] = "0";
  $numberTable[1.0] = "1";
  $numberTable[2.0] = "2";
  $numberTable[3.0] = "3";
  $numberTable[4.0] = "4";
  $numberTable[5.0] = "5";
  $numberTable[6.0] = "6";
  $numberTable[7.0] = "7";
  $numberTable[8.0] = "8";
  $numberTable[9.0] = "9";
  $numberTable[10.0] = "A";
  $numberTable[11.0] = "B";
  $numberTable[12.0] = "C";
  $numberTable[13.0] = "D";
  $numberTable[14.0] = "E";
  $numberTable[15.0] = "F";
  return $numberTable;
}
function stringToDecimal(&$string){
  return stringToDecimalForBase($string, 10.0);
}
function stringToDecimalForBase(&$string, $base){
  $stringToDecimalResult = new stdClass();
  $stringToDecimalResult->success = true;
  $i = 0.0;
  $isPositive = true;
  $beforeDecimalPoint = 0.0;
  $afterDecimalPoint = 0.0;
  $n = 0.0;
  $validCharacters = 0.0;

  if($base >= 2.0 && $base <= 16.0){
    for($j = 0.0; $j < count($string); $j = $j + 1.0){
      $c = $string[$j];
      if(isNumber($c, $base) || $c == "." || $c == "-"){
        $validCharacters = $validCharacters + 1.0;
      }
    }
    if($validCharacters == count($string)){
      if(count($string) > 0.0){
        $c = $string[$i];
        if($c == "-"){
          $isPositive = false;
          $i = $i + 1.0;
        }
        if($i < count($string)){
          $c = $string[$i];
          if(isNumber($c, $base)){
            for(; isNumber($c, $base) && ($i < count($string)); ){
              $beforeDecimalPoint = $beforeDecimalPoint + 1.0;
              $i = $i + 1.0;
              if($i < count($string)){
                $c = $string[$i];
              }
            }
            if($i < count($string)){
              $c = $string[$i];
              if($c == "."){
                $i = $i + 1.0;
                if($i < count($string)){
                  $c = $string[$i];
                  for(; isNumber($c, $base) && ($i < count($string)); ){
                    $afterDecimalPoint = $afterDecimalPoint + 1.0;
                    $i = $i + 1.0;
                    if($i < count($string)){
                      $c = $string[$i];
                    }
                  }
                }else{
                  $stringToDecimalResult->success = false;
                  $stringToDecimalResult->feilmelding = str_split("Number must have digits after the decimal point.");
                }
              }
            }
          }else{
            $stringToDecimalResult->success = false;
            $stringToDecimalResult->feilmelding = str_split("Number must start with digits (for negative numbers, after the optional negative sign).");
          }
        }
        if($stringToDecimalResult->success != false){
          $i = 0.0;
          if( !$isPositive ){
            $i = 1.0;
          }
          for($j = 0.0; $j < $beforeDecimalPoint; $j = $j + 1.0){
            $c = $string[$i];
            $i = $i + 1.0;
            $d = getDecimalFromSingleDecimalDigit($c, $base);
            $n = $n + $d*pow($base, $beforeDecimalPoint - $j - 1.0);
          }
          if($afterDecimalPoint > 0.0){
            $i = $i + 1.0;
            for($j = 0.0; $j < $afterDecimalPoint; $j = $j + 1.0){
              $c = $string[$i];
              $i = $i + 1.0;
              $d = getDecimalFromSingleDecimalDigit($c, $base);
              $n = $n + $d*pow($base, 0.0 - $j - 1.0);
            }
          }
          if( !$isPositive ){
            $n = -$n;
          }
          $stringToDecimalResult->result = $n;
          $stringToDecimalResult->success = true;
        }
      }else{
        $stringToDecimalResult->success = false;
        $stringToDecimalResult->feilmelding = str_split("String has no content.");
      }
    }else{
      $stringToDecimalResult->success = false;
      $stringToDecimalResult->feilmelding = str_split("String contains invalid character.");
    }
  }else{
    $stringToDecimalResult->success = false;
    $stringToDecimalResult->feilmelding = str_split("Base must be from 2 to 16.");
  }

  return $stringToDecimalResult;
}
function getDecimalFromSingleDecimalDigit($c, $base){
  $numberTable = getNumberTable();
  $position = 0.0;

  for($i = 0.0; $i < $base; $i = $i + 1.0){
    if($numberTable[$i] == $c){
      $position = $i;
    }
  }

  return $position;
}
function isNumber($c, $base){
  $numberTable = getNumberTable();
  $found = false;

  for($i = 0.0; $i < $base; $i = $i + 1.0){
    if($numberTable[$i] == $c){
      $found = true;
    }
  }

  return $found;
}
function assertFalse($b, $failures){
  if($b){
    $failures->doubleValue = $failures->doubleValue + 1.0;
  }
}
function assertTrue($b, $failures){
  if( !$b ){
    $failures->doubleValue = $failures->doubleValue + 1.0;
  }
}
function assertEquals($a, $b, $failures){
  if($a != $b){
    $failures->doubleValue = $failures->doubleValue + 1.0;
  }
}
function assertStringEquals(&$a, &$b, $failures){
  if( !stringsEqual($a, $b) ){
    $failures->doubleValue = $failures->doubleValue + 1.0;
  }
}
function &stringToNumberArray(&$string){
  $array = array_fill(0, count($string), 0);

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    $array[$i] = ord($string[$i]);
  }
  return $array;
}
function &numberArrayToString(&$array){
  $string = array_fill(0, count($array), 0);

  for($i = 0.0; $i < count($array); $i = $i + 1.0){
    $string[$i] = chr($array[$i]);
  }
  return $string;
}
function stringsEqual(&$data1, &$data2){
  $equal = false;
  if(count($data1) == count($data2)){
    $nrEqual = 0.0;
    for($i = 0.0; $i < count($data1); $i = $i + 1.0){
      if($data1[$i] == $data2[$i]){
        $nrEqual = $nrEqual + 1.0;
      }
    }
    if($nrEqual == count($data1)){
      $equal = true;
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function numberArraysEqual(&$data1, &$data2){
  $equal = false;
  if(count($data1) == count($data2)){
    $nrEqual = 0.0;
    for($i = 0.0; $i < count($data1); $i = $i + 1.0){
      if($data1[$i] == $data2[$i]){
        $nrEqual = $nrEqual + 1.0;
      }
    }
    if($nrEqual == count($data1)){
      $equal = true;
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function &substring(&$string, $from, $to){
  $n = array_fill(0, $to - $from, 0);

  for($i = $from; $i < $to; $i = $i + 1.0){
    $n[$i - $from] = $string[$i];
  }

  return $n;
}
function &appendString(&$string, &$s){
  $newString = array_fill(0, count($string) + count($s), 0);

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    $newString[$i] = $string[$i];
  }

  for($i = 0.0; $i < count($s); $i = $i + 1.0){
    $newString[count($string) + $i] = $s[$i];
  }

  unset($string);

  return $newString;
}
function &appendCharacter(&$string, $c){
  $newString = array_fill(0, count($string) + 1.0, 0);

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    $newString[$i] = $string[$i];
  }

  $newString[count($string)] = $c;

  unset($string);

  return $newString;
}
function &split(&$toSplit, $splitBy){
  $splitt = array_fill(0, 0.0, 0);

  $next = array_fill(0, 0.0, 0);
  for($i = 0.0; $i < count($toSplit); $i = $i + 1.0){
    $c = $toSplit[$i];
    if($c == $splitBy){
      $n = new stdClass();
      $n->string = $next;
      $splitt = addString($splitt, $n);
      $next = array_fill(0, 0.0, 0);
    }else{
      $next = appendCharacter($next, $c);
    }
  }

  if(count($next) > 0.0){
    $n = new stdClass();
    $n->string = $next;
    $splitt = addString($splitt, $n);
  }

  return $splitt;
}
function &addDecimal(&$list, $a){
  $newlist = array_fill(0, count($list) + 1.0, 0);
  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    $newlist[$i] = $list[$i];
  }
  $newlist[count($list)] = $a;
		
  unset($list);
		
  return $newlist;
}
function addDecimalRef($list, $i){
  $list->list = addDecimal($list->list, $i);
}
function &removeDecimal(&$list, $n){
  $newlist = array_fill(0, count($list) - 1.0, 0);

  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    if($i < $n){
      $newlist[$i] = $list[$i];
    }
    if($i > $n){
      $newlist[$i - 1.0] = $list[$i];
    }
  }
		
  unset($list);
		
  return $newlist;
}
function getDecimalRef($list, $i){
  return $list->list[$i];
}
function removeDecimalRef($list, $i){
  $list->list = removeDecimal($list->list, $i);
}
function &addString(&$list, $a){
  $newlist = array_fill(0, count($list) + 1.0, 0);

  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    $newlist[$i] = $list[$i];
  }
  $newlist[count($list)] = $a;
		
  unset($list);
		
  return $newlist;
}
function addStringRef($list, $i){
  $list->list = addString($list->list, $i);
}
function &removeString(&$list, $n){
  $newlist = array_fill(0, count($list) - 1.0, 0);

  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    if($i < $n){
      $newlist[$i] = $list[$i];
    }
    if($i > $n){
      $newlist[$i - 1.0] = $list[$i];
    }
  }
		
  unset($list);
		
  return $newlist;
}
function getStringRef($list, $i){
  return $list->list[$i];
}
function removeStringRef($list, $i){
  $list->list = removeString($list->list, $i);
}


echo test();