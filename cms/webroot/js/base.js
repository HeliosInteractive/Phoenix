// console.log polyfill
window.log = function() {
	log.history = log.history || [];
	log.history.push(arguments);
	if(this.console){
		console.log( Array.prototype.slice.call(arguments) );
	}
};

function BaseChannelPath() {
	var base_channel = "/helios/phoenix"
	return base_channel;
}

function SubChannelPath(path) {
	return BaseChannelPath() + "/" + path;
}

function AuthorizeMachine(info){
	$.post(cake.base_url + "machines/add", info, function(data) {
		window.location.reload();
	});
}

function AddMachineEntry(payload, columns) {
	try { var msg = $.parseJSON(payload); }
	catch(e) { log("Failed to parse: " + payload); return; }
	
	var row = $('<tr>');
	columns.forEach(function(col) {
		if (!MachineAuthorized(msg)) {
			if (col == "actions") {
				row.append($('<td>').append(
					$('<a>')
					.click(function() { AuthorizeMachine(msg); return false; })
					.text('Authorize'))
				);
			} else if (col == "status") {
				row.append($('<td>').text("Authorize this machine to see its stat."));
			} else {
				row.append($('<td>').text((col in msg) ? msg[col] : ''));
			}
		}
	});
	$('.content table')
		.find('tbody')
		.append(row);
}

function MachineAuthorized(obj) {
	if (registered_systems == undefined)
		return false;
	
	var found = false;
	registered_systems.forEach(function(val) {
		found = (val.name == obj.name && val.public_key == obj.public_key);
	});
	
	return found;
}

function HandlePing(payload) {
	try { var msg = $.parseJSON(payload); }
	catch(e) { log("Failed to parse: " + payload); return; }
	
	$('.content table tbody tr')
		.each(function( index, value ) {
			var elem = $(value);
			if (elem.text().length == 0)
				return;
			
			if ($(".name", elem).text() == msg.name) {
				var status = "<b>online</b>";
				status += "<br />cpu: " + (msg.cpu * 100).toFixed(2) + "%";
				status += "<br />mem: " + (msg.mem * 100).toFixed(2) + "%";
				$(".status", elem).html(status);
				$(elem).attr("ping", Date.now());
			}
		});
	
	console.log(msg);
}

function HandleOfflines(dead_interval) {
	$('.content table tbody tr')
		.each(function( index, value ) {
			var elem = $(value);
			if (elem.text().length == 0)
				return;
			
			var last_ping = $(elem).attr("ping");
			if (typeof last_ping !== typeof undefined && last_ping !== false) {
				last_ping = +last_ping;
			} else {
				last_ping = 0;
			}
			
			if (Date.now() - last_ping >= dead_interval)
				$(".status", elem).html("<b>offline</b>");
		});
}

$(document).ready(function() {
	var columns = [];
	var thead = $('.content table thead tr th')
		.each(function( index, value ) {
			var header = $('a', value).length == 1 ? $('a', value).text() : $(value).text();
			header = header.toLowerCase().replace(' ', '_');
			columns.push(header);
		});
	
	var client 			= mqtt.connect("ws://192.168.56.1:8080/");
	var echo_channel 	= SubChannelPath("echo");
	var ping_channel 	= SubChannelPath("ping");
	var ping_interval 	= 3 * 1000;
	var dead_interval 	= 5 * ping_interval;
	
	client.subscribe(echo_channel);
	client.subscribe(ping_channel);

	client.on("message", function(topic, payload) {
		if (topic == echo_channel)
			AddMachineEntry(payload, columns);
		else if (topic == ping_channel)
			HandlePing(payload);
	});

	client.publish(BaseChannelPath(), "echo");
	client.publish(BaseChannelPath(), "ping");
	HandleOfflines(dead_interval);
	
	setInterval(function() {
		client.publish(BaseChannelPath(), "ping");
	}, ping_interval);
	
	setInterval(function() {
		HandleOfflines(dead_interval);
	}, ping_interval);
});
