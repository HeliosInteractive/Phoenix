function BaseChannelPath() {
	var base_channel = "/helios/phoenix"
	return base_channel;
}

function SubChannelPath(path) {
	return BaseChannelPath() + "/" + path;
}

function AddMachineEntry(payload, columns) {
	try { var msg = $.parseJSON(payload); }
	catch(e) { console.log("Failed to parse: " + payload); return; }
	
	var row = $('.content table')
			.find('tbody')
			.append($('<tr>'));
	
	columns.forEach(function(col) {
		row.append($('<td>').text((col in msg) ? msg[col] : ''));
	});
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
			console.log([topic, payload].join(" : "));
		
		client.end();
	});

	client.publish(BaseChannelPath(), "echo");
});
