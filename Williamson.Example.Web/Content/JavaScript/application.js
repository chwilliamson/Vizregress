/// <reference path="jquery-1.8.2.js"/>
/// <reference path="jquery-ui-1.9.1.custom.js"/>

Williamson = function () { }
Williamson.ExampleApplication = function () { }
Williamson.ExampleApplication.start = function () {
    $().ready(function () {
        var $dialogEl = $('<span data-bind="template: { name: \'dialog-template\' }"></span>');
        $('#appContainer').append($dialogEl);
        function Model() {
            this.title = "What about a Tour";
            this.text = "Would you like a tour?";
            this.id = "dialog";
        }
        ko.applyBindings(new Model());
        function remove() {
            $dialogEl.remove();
        }
        
        $('#dialog').dialog({
            modal: true,
            width: 600,
            resizeable: false,
            buttons: {
                "Yes please!": function () {
                    $(this).dialog("close");
                    remove();
                    new Williamson.Tour().begin();
                },
                "No thanks": function () {
                    $(this).dialog("close");
                    remove();
                }

            }
        });
    })
}

/**
* Takes the user through a quick tour.
*/
Williamson.Tour = function () {
    this.$stepEl = $('<span data-bind="template: { name: \'step-template\' }"></span>');
    $('#appContainer').append(this.$stepEl);

}
Williamson.Tour.prototype = {
    /**
    Start the tour
    */
    begin: function () {
        var self = this;
        this._apply(
            "Using Web API Self Host",
            "This application is hosted using the ASP.NET SelfHost with a few hacks. Don\'t rely on it",
            function() {
                alert('previous');
            },
            function () {
                //go next step
                self.step2();
            }
        );
    },
    step2: function () {
        alert('todo:step2');
    }
    ,
    _apply: function(title, text,previousClick, nextClick, previousDisabled, nextDisabled) {
        if (previousDisabled == undefined || !previousDisabled) previousDisabled = false;
        if (nextDisabled == undefined || !nextDisabled) nextDisabled = false;
        ko.applyBindings({
            title: title,
            text: text
            
        }, this.$stepEl.get(0));

        this.$stepEl.find('#nextBtn').button({ disabled: previousDisabled }).click(nextClick);
        this.$stepEl.find('#previousBtn').button({ disabled: nextDisabled }).click(previousClick);
    }
}
