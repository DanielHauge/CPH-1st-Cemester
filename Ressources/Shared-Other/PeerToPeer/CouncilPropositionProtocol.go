package main

import (
	"github.com/boltdb/bolt"
	"bytes"
	"log"
)

func EvaluateProposition(block *Block, user string){

	var lastHash []byte

	err := BlockChain.db.View(func(tx *bolt.Tx) error{
		b:= tx.Bucket([]byte("blocksBucket"))
		lastHash = b.Get([]byte("1"))

		return nil
	})
	if err != nil{
		log.Println("Something went wrong at join to get last hash")
	}

	PoW := GenerateProofOfWork(block)
	if (PoW.InspectGemCarat() && bytes.Equal(lastHash, block.PrevBlockHash)){
		DiscussionBlock = PoW
		Proposal := createMessage("JOIN-PROPOSITION-ANSWER", Name, getMyIP(), "Yes", make([]string, 0), make([]string, 0))
		Proposal.sendPrivate(user)
	} else {
		Proposal := createMessage("JOIN-PROPOSITION-ANSWER", Name, getMyIP(), "NO!", make([]string, 0), make([]string, 0))
		Proposal.sendPrivate(user)
	}
}

func CountVotes(who string, answer string){
	if answer == "Yes" {
		log.Println("Vote count: Yes!")

		DiscussionAgreement[who] = true

	} else {
		log.Println("HeadCount Status: NO! from: "+who)
		DiscussionAgreement[who] = false
	}
}
