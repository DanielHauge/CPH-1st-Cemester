package main

import (
	"bytes"
	"math/big"
	"encoding/json"
	"log"
	"strconv"
	"crypto/sha256"
	"time"
)

type Block struct {
	Timestamp		int64
	Data 			[]byte
	PrevBlockHash 	[]byte
	Hash			[]byte
	Nounce			int
}

func (b *Block) Serialize() []byte {
	var result bytes.Buffer
	enc := json.NewEncoder(&result)
	err := enc.Encode(b)
	if err != nil {log.Println("Something went wrong with encoding block to json byte array")}

	return result.Bytes()
}

func Deserialize(d []byte) *Block {
	var block Block

	dec := json.NewDecoder(bytes.NewReader(d))
	err := dec.Decode(&block)
	if err != nil {log.Println("Something went wrong with decoding block to struct with json")}

	return &block
}

type ProofOfWork struct {
	block *Block
	target *big.Int
}


func (b *ProofOfWork) PoWSerialize() []byte {
	var result bytes.Buffer
	enc := json.NewEncoder(&result)
	err := enc.Encode(b)
	if err != nil {log.Println("Something went wrong with encoding POW to json byte array")}

	return result.Bytes()
}

func PoWDeserialize(d []byte) *ProofOfWork {
	var pow ProofOfWork

	dec := json.NewDecoder(bytes.NewReader(d))
	err := dec.Decode(&pow)
	if err != nil {log.Println("Something went wrong with decoding POW to struct with json")}

	return &pow
}

/*
This is just a generic method for getting a new block struct. It will take some data and timestamp(ID) and the previus blocks hash
This will be called by addblock method of the blockchain struct.
 */
func NewBlock (data string, preBlockHash []byte) *Block{
	block := &Block{time.Now().Unix(), []byte(data), preBlockHash, []byte{}, 0}
	//block.SetHash()
	pow := GenerateProofOfWork(block)
	nounce, hash := pow.Mine()
	block.Hash = hash[:]
	block.Nounce = nounce
	return block
}

/*
You cannot add a block to the chain without a previus block, it will go out of index if there's no previus block, therefor this method is to birth a blockchain without a previus block. This block is often called genesis block in the blockchain world.
 */
func BirthFirstBlock() *Block {
	return NewBlock("Genesis Block", []byte{})
}

/*
This will generate a ProofOfWork struct
 */
func GenerateProofOfWork(b *Block) *ProofOfWork{
	target := big.NewInt(1)
	target.Lsh(target, uint(256-targetBits))
	pow := &ProofOfWork{b, target}
	return pow
}










/*
Old mining (set hash) function for the block)
Function SetHash() will be called with a Block struct so:
lets say we have
MyCoolBlock Block
we can do
MyCoolBlock.SetHash() -> This will calculate the hash of whatever is in the block > mining / Proof of work will come later
BUUUUT, we will not call this method primitively, it will be called with a Blockchain struct -> We kinda need a previus block of the blockchain to add a block right.

Note: This is the primitive way to just calculate the hash of whatever there is, no nounce or any proof of work: See SetupMineCart and Mine functions for that.
 */
func (b *Block) SetHash() {
	t_stamp := []byte(strconv.FormatInt(b.Timestamp, 10))
	headers := bytes.Join([][]byte{b.PrevBlockHash, b.Data, t_stamp}, []byte{})
	hash := sha256.Sum256(headers)

	b.Hash = hash[:]
}