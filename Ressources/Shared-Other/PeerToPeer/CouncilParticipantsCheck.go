package main

import (
	"log"
)

func ShareMyFellowPeeps(speaker string, speakersUsernames []string, speakerport string){

	Users, IPS := getFromMap(PeerIPs)
	DiscussionInSession = true
	DiscussionSpeakerPort = speakerport
	DiscussionSpeaker = speaker
	if (CrossCheckList(Users, speakersUsernames) && len(Users) == len(speakersUsernames)){

		HeadCountMessage := createMessage("SESSION-HEAD-COUNT", Name, getMyIP(), "Yes", Users, IPS)
		HeadCountMessage.sendPrivate(speaker)
	} else {
		HeadCountMessage := createMessage("SESSION-HEAD-COUNT", Name, getMyIP(), "No", Users, IPS)
		HeadCountMessage.sendPrivate(speaker)
	}

}

func CrossCheckList(list1 []string, list2 []string) bool{
	result := true

	for _, b := range list1{

		if !DoesUserExist(b, list2){
			result = false
			if debugging{log.Println("User didn't exist!: "+b)}

			if b == Name || b == DiscussionSpeaker{
				result = true
				if debugging{log.Println("Ohh it was Speaker or Me")}
			}
		}
	}


	return result
}
func DoesUserExist(name string, strings []string) bool {
	result := false;

	for _, b := range strings{
		if b == name {
			result = true

		}
		if debugging{log.Println(b)}
	}


	return result
}

func HeadCount(who string, answer string, users []string){
	if answer == "Yes" {
		if debugging{log.Println("HeadCount Status: Yes!")}
		Users, _ := getFromMap(PeerIPs)
		if CrossCheckList(users, Users){
			if debugging{log.Println("Yes CrossCheck was correct")}
			DiscussionAgreement[who] = true
			DiscussionParticipants = append(DiscussionParticipants, who)
		} else {
			log.Println("HeadCount Status: NO!")
			DiscussionAgreement[who] = false
			DiscussionParticipants = append(DiscussionParticipants, who)
		}

	} else {
		log.Println("HeadCount Status: NO!")
		DiscussionAgreement[who] = false
		DiscussionParticipants = append(DiscussionParticipants, who)
	}
}

func CountCouncilMembersDecision() int{
	result := len(DiscussionAgreement)

	for _, b := range DiscussionAgreement{
		if b{
			result--
		}
	}


	return result
}