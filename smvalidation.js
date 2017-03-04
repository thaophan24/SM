<script>
// add rule
$.validator.addMethod('smval', function(value, element, params){
	return value == params.pr1;
});
// add rule to adapters
$.validator.unobtrusive.adapters.add('smval', ['pr1'], function(options){
	options.rules['smval'] = options.params;
	options.messages['smval'] = options.message;
});
</script>