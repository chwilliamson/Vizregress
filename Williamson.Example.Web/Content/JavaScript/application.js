/// <reference path="jquery-1.8.2.js"/>
/// <reference path="jquery-ui-1.9.1.custom.js"/>
/// <reference path="underscore.js"/>

//templates for everything!

var templates = {
    _span: _.template("<div class=\"span<%= n %>\"><%= inner %></div>"),
    _span12: function(inner) {
        return templates._span({ n: 12, inner:inner });
    },
    dialog: _.template("<div id=\'<%= id %>\' title=\'<%= title %>\'><p><%= text %></p></div>"),
    tour: {
        _stepBody: function (title, text) { return _.template("<h2><%= title %></h2><%= text %>")({title:title,text:text}) },
        _tpl: function (title, text) { return templates._span12(templates.tour._stepBody(title,text)); },
        step1: function () { return templates.tour._tpl('Using Web API Self Host','step1'); }
    } 
};



williamson = function () { }
williamson.templateHelper = function () { }
williamson.templateHelper.prototype = {
    
}
exampleApplication = function () { }
exampleApplication.start = function () {
    $().ready(function () {
        $('#appContainer')
            .append(templates.dialog({ id: 'dialog', title: 'Welcome', text: 'Would you like a tour about the self hosted container?' }));

        $('#dialog').dialog({
            modal: true,
            width:600,
            resizeable:false,
            buttons: {
                "Yes please!": function () {
                    $(this).dialog("close");
                    new tour().begin();
                },
                "No thanks": function () {
                    $(this).dialog("close");
                }            
        
            }
        });
    });    
}

tour = function() {
}

tour.prototype = {
    /**
    Start the tour
    */
    begin: function () {
        $('#appContainer')
           .append(templates.tour.step1());
    }
}
