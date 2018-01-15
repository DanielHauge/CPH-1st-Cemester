package main

import (
	"github.com/prometheus/client_golang/prometheus"
	"net/http"
	"github.com/prometheus/client_golang/prometheus/promhttp"
)

var (
	PromSpeaker = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "Am_I_Speaker",
			Help: "This represent if speaker or not",
		},
	)

	PromInSession = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "Am_I_Discussing",
			Help: "This represent if discussing or not",
		},
	)

	InboundTCP = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "tcp_inbound",
			Help: "The ammount of inbound TCP.",
		},
	)

	OutboundTCP = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "tcp_outbound",
			Help: "The ammount of inbound TCP.",
		},
	)

	PromDiscussionParticipants = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "discussion_participants",
			Help: "This represent if discussing participants",
		},
	)

	ENDOutboundTCP = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "tcp_outbound_END",
			Help: "The ammount of ends.",
		},
	)

	ABORTOutboundTCP = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "tcp_outbound_ABORT",
			Help: "failed aborts.",
		},
	)

	INVOutboundTCP = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "tcp_outbound_INV",
			Help: "The ammount of invites.",
		},
	)

	JoinsOrLeaves = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "join_or_leave",
			Help: "a proposition has been made by this node!",
		},
	)

	QueueSize = prometheus.NewGauge(
		prometheus.GaugeOpts{
			Name: "queue_size",
			Help: "how many discussions are in queue!!",
		},
	)




)

func init(){
	prometheus.MustRegister(INVOutboundTCP)
	prometheus.MustRegister(ABORTOutboundTCP)
	prometheus.MustRegister(ENDOutboundTCP)
	prometheus.MustRegister(PromDiscussionParticipants)
	prometheus.MustRegister(OutboundTCP)
	prometheus.MustRegister(InboundTCP)
	prometheus.MustRegister(PromSpeaker)
	prometheus.MustRegister(PromInSession)
	prometheus.MustRegister(JoinsOrLeaves)
	prometheus.MustRegister(QueueSize)

}


func ServceMetrics (w http.ResponseWriter, r *http.Request){

	PromDiscussionParticipants.Set(float64(len(DiscussionParticipants)))
	QueueSize.Set(float64(len(DiscussionQueue)))
	promhttp.Handler().ServeHTTP(w, r)
	JoinsOrLeaves.Set(0)
	INVOutboundTCP.Set(0)
	ABORTOutboundTCP.Set(0)
	ENDOutboundTCP.Set(0)
	InboundTCP.Set(0)
	OutboundTCP.Set(0)
}