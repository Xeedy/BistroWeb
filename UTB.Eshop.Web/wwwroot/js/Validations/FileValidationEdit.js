$.validator.addMethod('filecontent', function (value, element, params) {
    var fileContentType = params[1];

    // Check if a file is selected
    if (element && element.files && element.files.length > 0) {
        var fileContentTypeFromFile = element.files[0].type;

        // Validate the file type
        if (fileContentTypeFromFile && fileContentTypeFromFile !== "" && fileContentTypeFromFile.toLowerCase().includes(fileContentType.toLowerCase())) {
            return true;
        }
    } else {
        // Skip validation if no file is selected
        return true;
    }

    return false;
});

$.validator.unobtrusive.adapters.add('filecontent', ['type'], function (options) {
    var element = $(options.form).find('#fileupload')[0];

    options.rules['filecontent'] = [element, options.params['type']];
    options.messages['filecontent'] = options.message;
});
