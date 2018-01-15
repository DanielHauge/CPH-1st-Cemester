package main

import (
	"bytes"
	"fmt"
	"math/big"
	"math"
	"crypto/sha256"
	"log"
)

const targetBits = 24


/*
This will setup the Proof of Work enviroment, with data and everything to be ready to be mined. You will need to initialize a nounce to be started with
 */
func (pow *ProofOfWork) SetupMineCart(nounce int) []byte{
	data := bytes.Join(
		[][]byte{
			pow.block.PrevBlockHash,
			pow.block.Data,
			IntToHex(pow.block.Timestamp),
			IntToHex(int64(targetBits)),
			IntToHex(int64(nounce)),
		}, []byte{},
	)

	return data
}

/*
Quick dirty method for handling INT Conversion to HEX, didn't know how to do it when i wrote setup mine cart stuff.
 */
func IntToHex(i int64) []byte {
	hex := fmt.Sprintf("%x", i)
	return []byte(hex)
}


/*
Starts to calculate Hash on the nounce, and will ofcause itterate through with all possible nounce, untill it reaches it's target.
 */
func (pow *ProofOfWork) Mine() (int, []byte){
	var hashInt big.Int
	var hash [32]byte
	nounce := 1

	log.Println("Mining block with: "+string(pow.block.Data))
	maxNounce := math.MaxInt32
	for nounce < maxNounce {
		data := pow.SetupMineCart(nounce)
		hash = sha256.Sum256(data)
		hashInt.SetBytes(hash[:])

		if hashInt.Cmp(pow.target) == -1 {
			break
		} else {
			nounce++
		}

	}

	return nounce, hash[:]
}


/*
Validation of the Hash method. Just checking if the nounce is actully producing the correct hash (Within target / requirements)
 */
func (pow *ProofOfWork) InspectGemCarat() bool {
	var hashInt big.Int

	data := pow.SetupMineCart(pow.block.Nounce)
	hash := sha256.Sum256(data)
	hashInt.SetBytes(hash[:])

	TrueCarat := hashInt.Cmp(pow.target) == -1

	return TrueCarat
}
