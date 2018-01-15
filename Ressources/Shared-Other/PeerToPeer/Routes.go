package main

import "net/http"

type Route struct {
	Name        string
	Method      string
	Pattern     string
	HandlerFunc http.HandlerFunc
}

type Routes []Route

var routes = Routes{
	Route{
		"Index",
		"GET",
		"/",
		Status,
	},

	Route{
		"Join",
		"GET",
		"/join",
		Join,
	},

	Route{
		"Leave queue",
		"GET",
		"/leave",
		UnJoin,
	},

	Route{
		"Status",
		"GET",
		"/status",
		Status,
	},

	Route{
		"Answer",
		"POST",
		"/answer",
		AnswerOffer,
	},

	Route{
		"Move Out",
		"GET",
		"/moveout",
		MoveOut,
	},
	Route{
		"Chain",
		"GET",
		"/chain",
		GetChain,
	},
	Route{
		"List Queue",
		"GET",
		"/queue",
		GetQueue,
	},
	Route{
		"SIM-Get",
		"GET",
		"/sim",
		GetSimulationData,
	},
	Route{
		"Metrics",
		"GET",
		"/metrics",
		ServceMetrics,
	},

}