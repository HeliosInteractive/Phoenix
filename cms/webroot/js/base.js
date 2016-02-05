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
	console.log(info);
	$.post("/machines/authorize", info, function(data) {
		log("Machine authorized: " + data);
	});
}

function AddMachineEntry(payload, columns) {
	try { var msg = $.parseJSON(payload); }
	catch(e) { log("Failed to parse: " + payload); return; }
	
	var row = $('<tr>');
	columns.forEach(function(col) {
		if (col == "is_authorized") {
			row.append($('<td>').append(
				$('<a>')
				.click(function() { AuthorizeMachine(msg); return false; })
				.text('authorize'))
			);
		} else {
			row.append($('<td>').text((col in msg) ? msg[col] : ''));
		}
	});
	$('.content table')
		.find('tbody')
		.append(row);
}

$(document).ready(function() {
	var columns = [];
	var thead = $('.content table thead tr th')
		.each(function( index, value )
		{
			var header = $('a', value).length == 1 ? $('a', value).text() : $(value).text();
			header = header.toLowerCase().replace(' ', '_');
			columns.push(header);
		});
	
	var client = mqtt.connect("ws://test.mosquitto.org:8080/");
	client.subscribe(SubChannelPath("machines"));

	client.on("message", function(topic, payload) {
		if (topic == SubChannelPath("machines"))
			AddMachineEntry(payload, columns);
		else
			log([topic, payload].join(" : "));
		
		client.end();
	});

	client.publish(BaseChannelPath(), "echo");
});
