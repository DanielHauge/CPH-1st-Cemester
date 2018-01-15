package main

import (
	"fmt"
	"net/http"
	"log"
	"strconv"
 	"github.com/shurcooL/github_flavored_markdown"
	"bytes"
	"gopkg.in/russross/blackfriday.v2"
	"encoding/json"
)

func Index(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "Welcome!")
	log.Println("Peers IP's: ",PeerIPs)
	log.Println("Connections: ",Connections)
	msg := createMessage("YELL", Name, getMyIP(), "HEEEEELOOOOO WELCOME!", make([]string, 0), make([]string, 0))
	msg.send_all()
}

func Join(w http.ResponseWriter, r *http.Request){


	dis := createDiscussion(Name, Name+":join")
	go StartDiscussion(dis)
	fmt.Fprintf(w, "Data: %v\n", "Discussion Started", dis.Data)
	JoinsOrLeaves.Inc()
}



func UnJoin(w http.ResponseWriter, r *http.Request){

	dis := createDiscussion(Name, Name+":leave")
	go StartDiscussion(dis)
	fmt.Fprintf(w, "Data: %v\n", "Discussion Started", dis.Data)
	JoinsOrLeaves.Inc()
}

func Status(w http.ResponseWriter, r *http.Request){

	var buffer bytes.Buffer
	buffer.WriteString("# BlockLand Status!\n\n")
	buffer.WriteString("## Username: **"+Name+"**\n\n")
	buffer.WriteString("----------------------\n\n")
	buffer.WriteString("----------------------\n\n")
	buffer.WriteString("## Network\n\n")
	buffer.WriteString("| User              | IP                  | TCP Connection          |\n")
	buffer.WriteString("| ----------------- |:-------------------:| -----------------------:|\n")
	for a, b := range PeerIPs{
		constring := fmt.Sprintf("%v", Connections[a])
		buffer.WriteString("| "+a+" | "+b+" | "+constring+" |\n")
	}
	buffer.WriteString("\n## Discussion\n\n")
	buffer.WriteString("\n##### Discussion queue: "+strconv.Itoa(len(DiscussionQueue))+"\n\n")

	if DiscussionInSession {
		buffer.WriteString("### Discussion in session - Topic: "+DiscussionTopic+"\n\n")
		buffer.WriteString("###### Speaker: "+DiscussionSpeaker+"\n\n")

		if Name == DiscussionSpeaker{

			buffer.WriteString("##### Participants\n\n")
			for _, b := range DiscussionParticipants{
				buffer.WriteString(b+", ")
			}
			buffer.WriteString("\n\n##### Current Agreements\n\n")
			buffer.WriteString("| User              | Vote                |\n")
			buffer.WriteString("| ----------------- |:-------------------:|\n")
			for a, b := range DiscussionAgreement{
				buffer.WriteString("| "+a+" | "+strconv.FormatBool(b)+" |\n")
			}
			buffer.WriteString("\n")

		}else {
			buffer.WriteString("Go to ```"+PeerIPs[DiscussionSpeaker]+DiscussionSpeakerPort+"/status``` for more info\n\n")
		}


	} else {
		buffer.WriteString("No discussion is in session\n\n")
	}

	markdown := blackfriday.Run([]byte(buffer.String()))
	w.Write(markdown)

}

func AnswerOffer(w http.ResponseWriter, r *http.Request){

}

func MoveOut(w http.ResponseWriter, r *http.Request){

}

func GetChain(w http.ResponseWriter, r *http.Request){

	var buffer bytes.Buffer

	buffer.WriteString("# Full Blockchain\n\n")
	buffer.WriteString("User: "+Name+"\n\n")
	buffer.WriteString("| Index | Nounce | Data              | Hash                         | Prev. hash                      | Proof of Work |\n")
	buffer.WriteString("|:-----:|:-------------:|:--------------------------- |:--------------------------------------------------- |:----------------------------------------------------------- |:-------------:|\n")
	bcreader := BlockChain.GetReader()
	test := 0
	for {
		block := bcreader.Next()
		test ++
		pow := GenerateProofOfWork(block)
		buffer.WriteString("| "+fmt.Sprintf("%x", test)+" | "+strconv.Itoa(block.Nounce)+" | "+fmt.Sprintf("%s", block.Data) + " | "+ fmt.Sprintf("%x", block.Hash)+" | "+fmt.Sprintf("%x", block.PrevBlockHash)+" | "+fmt.Sprintf("%s", strconv.FormatBool(pow.InspectGemCarat()))+" |\n")

		if len(block.PrevBlockHash) == 0 {
			break
		}
	}


	markdown := blackfriday.Run([]byte(buffer.String()))
	rendered := github_flavored_markdown.Markdown(markdown)
	w.Write(rendered)
}

func GetQueue(w http.ResponseWriter, r *http.Request){

	var buffer bytes.Buffer
	queue := ConstructQueue()
	mypos := "\n\n### You are not in queue\n\n"
	buffer.WriteString("# Block-Land Queue\n\n")
	buffer.WriteString("User: "+Name+"\n\n")
	buffer.WriteString("| Queue Number | User |\n")
	buffer.WriteString("|:------------:| ----:|\n")
	if debugging{ log.Println(len(queue))}

	for i, u := range queue{
		if debugging{ log.Println(strconv.Itoa(i)+" - "+u)}

		buffer.WriteString("| "+strconv.Itoa(i+1)+" | "+u+" |\n")
		if (u == Name){
			mypos = "\n\n### You are at position: "+strconv.Itoa(i+1)+"\n\n"
		}
	}
	buffer.WriteString(mypos)

	markdown := blackfriday.Run([]byte(buffer.String()))
	rendered := github_flavored_markdown.Markdown(markdown)
	w.Write(rendered)
}

func GetSimulationData(w http.ResponseWriter, r *http.Request){
	qu := ConstructQueue()
	n := 0
	if debugging{log.Println(len(qu))}
	for i, u := range qu{
		if debugging{log.Println(u) }

		if u == Name{
			n = i+1
		}
	}

	QS := &QueueStatus{Queue:qu,Queuenumber:n}
	if debugging{log.Println(QS.Queue[0]) }

	msgs, err := json.Marshal(QS); if err != nil {log.Println(err.Error())}
	if debugging{ log.Println(string(msgs))}

	fmt.Fprint(w, string(msgs))
}
