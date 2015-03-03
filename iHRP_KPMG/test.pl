#!/usr/bin/perl

##
#   RWKG
#   Random WEP/WPA Keys Generator
#
#   This tool generate a random string of
#   allowed ascii characters and convert it
#   to hex format (5/13/16/29 characters are 
#   necessary to create 64/128/152/256 bits 
#   WEP keys, 8-63 characters strings instead
#   to create WPA/PSK keys).
#
#   Code for SecurityWireless.info proof use only.
#
#   Legal notes :
#   BlackAngels staff refuse all responsabilities 
#   for an incorrect or illegal use of this software 
#   or for eventual damages to others systems.
##


# Variables
$keytype = $ARGV[ 0 ];
$keydimension = $ARGV[ 1 ];
$string = " !#\$\%\\&()*+,-./0123456789:;<=>?\@ABCDEFGHIJKLMNOPQRSTUVWXYZ\[^]_abcdefghijklmnopqrstuvwxyz{~|}";
@chars = split(//,$string);
$n = @chars - 1;


# Random WEP/WPA Keys Generator
if ($keytype eq "WEP") {
                         if ($keydimension eq 64) { $key = generate(5); }
                         elsif ($keydimension eq 128) { $key = generate(13); }
                         elsif ($keydimension eq 152) { $key = generate(16); }
                         elsif ($keydimension eq 256) { $key = generate(29); }
                         else { usage(); }

                         print "\n\n[*] Generating WEP key ...\n";
                         $type = "WEP";
                       }
elsif ($keytype eq "WPA") {
                            if ($keydimension >= 8 && $keydimension <= 63) { 
                                                                             if ($keydimension <= 20) { print "\nWarning: WPA/PSK keys with a length of less then 20 characters, could be simply found via bruteforce attack ..."; }
                                                                             $key = generate($keydimension) 
                                                                           }
                            else { usage(); }

                            print "\n\n[*] Generating WPA/PSK key ...\n";
                            $type = "WPA/PSK";
                          }
else { usage(); } 

$hex = unpack('H*', $key);
print "\n[+] Random $type key";
print "\n                   [-] ASCII Value -> $key";
print "\n                   [-] Hex Value   -> $hex\n\n";


# Subroutines
sub generate
{
  my ($len) = @_;
  for ($i=0;$i<$len;$i++) {
                            $index = int(rand $n);
                            $key = $key . $chars[$index];
                          }
  return $key;
  exit(1);
}

sub usage
{
  print "\nUsage :\n";
  print "perl rwkg.pl <key type> <key dimension>\n";
  print "Key types allowed: WEP, WPA\n";
  print "Key dimension WEP allowed values: 64, 128, 152, 256\n";
  print "Key dimension WPA/PSK allowed values: 8/63\n";
  print "Code for SecurityWireless.info proof use only\n\n";
  exit(1);
}