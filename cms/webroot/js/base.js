/*!
 * @fn        log
 * @namespace window
 * @brief     A console.log polyfill
 */
window.log = function()
{
	if(this.console) {
		console.log( Array.prototype.slice.call(arguments) );
	}
};

/*!
 * @class MqttChannelManager
 * @brief Handles sub channel path construction based on a base channel.
 */
function MqttChannelManager(base_path)
{
	var base = base_path;
	
	this.getBase = function() { return base; }
	this.getSub = function(p) { return base + "/" + p; }
	this.getEcho = function() { return this.getSub("echo"); }
	this.getPing = function() { return this.getSub("ping"); }
}

/*!
 * @class Machines
 * @brief Holds info about client machines, can authorize/deauthorize
 * them and syncs up their information with CakePHP's printed table.
 */
function Machines(cols)
{
	var columns = cols;
	
	var authorize = function(info) {
		$.post(config.base_url + "machines/add", info, function(data) {
			window.location.reload();
		});
	}
	
	var isAuthorized = function(obj) {
		if (registered_systems == undefined)
			return false;
		
		var found = false;
		registered_systems.forEach(function(val) {
			found = (val.name == obj.name && val.public_key == obj.public_key);
		});
		
		return found;
	}
	
	this.add = function(payload) {
		try { var msg = $.parseJSON(payload); }
		catch(e) { log("Failed to parse: " + payload); return; }
		
		var row = $('<tr>');
		columns.forEach(function(col) {
			if (!isAuthorized(msg)) {
				if (col == "actions") {
					row.append($('<td>').append(
						$('<a>')
						.click(function() { authorize(msg); return false; })
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
				var monitoring = msg.monitoring ? "monitoring" : "dead";
				var status = "online <b>(" + monitoring + ")</b>";
				status += "<pre>";
				status += "\ncpu: " + (msg.cpu * 100).toFixed(2) + "%";
				status += "\ngpu: " + (msg.gpu * 100).toFixed(2) + "%";
				status += "\nram: " + (msg.ram * 100).toFixed(2) + "%";
				status += "</pre>";
				$(".status", elem).html(status);
				$(elem).attr("ping", Date.now());
			}
		});
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

window.Command = function(action, name) {
	/* no-op */
}

$(document).ready(function() {
	
	if (config.mqtt_url == "")
		return;
	
	var columns = [];
	$('.content table thead tr th').each(function( index, value ) {
		var header = $('a', value).length == 1 ? $('a', value).text() : $(value).text();
		header = header.toLowerCase().replace(' ', '_');
		columns.push(header);
	});
	
	var channel_mgr 	= new MqttChannelManager("/helios/phoenix")
	var machines		= new Machines(columns);
	var client			= mqtt.connect(config.mqtt_url);
	var ping_interval 	= 3 * 1000;
	var dead_interval 	= 5 * ping_interval;
	
	client.subscribe(channel_mgr.getEcho());
	client.subscribe(channel_mgr.getPing());
	
	client.on("connect", function() {
		$(".machines h3").text("Machines (connected)");
	});
	client.on("close", function() {
		$(".machines h3").text("Machines (disconnected)");
	});
	
	client.on("message", function(topic, payload) {
		if (topic == channel_mgr.getEcho())
			machines.add(payload, columns);
		else if (topic == channel_mgr.getPing())
			HandlePing(payload);
	});
	
	window.Command = function(action, name) {
		client.publish(SubChannelPath(name), action);
	}

	client.publish(channel_mgr.getBase(), "echo");
	client.publish(channel_mgr.getBase(), "ping");
	HandleOfflines(dead_interval);
	
	setInterval(function() {
		client.publish(channel_mgr.getBase(), "ping");
	}, ping_interval);
	
	setInterval(function() {
		HandleOfflines(dead_interval);
	}, ping_interval);
});
