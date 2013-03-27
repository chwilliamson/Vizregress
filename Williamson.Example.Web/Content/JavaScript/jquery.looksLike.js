

jQuery._callLooksLike = function(info) {

};

/**
* Determines if an element visually looks like
* a previous captured test.
*/
jQuery.looksLike = function(selector, version , name) {
    
    //capture without zone
    jQuery._callLooksLike({version:version, name: name, ZoneMode: 'off' });
    
    //show with zone and returns whether or not a match is found
    return jQuery._callLooksLike({version:version, name: name, ZoneMode: 'on' });

}