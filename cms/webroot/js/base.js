$(document).ready(function() {
	console.log("here");
	var client = mqtt.connect("ws://test.mosquitto.org:8080/");
	client.subscribe("/helios/phoenix");

	client.on("message", function(topic, payload) {
		console.log([topic, payload].join(": "));
		client.end();
	});

	client.publish("/helios/phoenix", "hi!");
});
