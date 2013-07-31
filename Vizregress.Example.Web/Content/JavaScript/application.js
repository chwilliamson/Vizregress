/// <reference path="jquery-1.8.2.js"/>
/// <reference path="jquery-ui-1.9.1.custom.js"/>
/// <reference path="knockout.js"/>

Williamson = function() {
};
Williamson.ExampleApplication = function() {
};
Williamson.ExampleApplication.start = function() {
    $().ready(function() {
        var $dialogEl = $('<span data-bind="template: { name: \'dialog-template\' }"></span>');
        $('#appContainer').append($dialogEl);

        ko.applyBindings(
            { title: "What about a Tour", text: "Would you like a tour?" }, $dialogEl.get(0));

        function remove() {
            $dialogEl.remove();
        }

        $dialogEl.dialog({
            modal: true,
            width: 600,
            resizeable: false,
            buttons: {
                "Yes please!": function() {
                    $(this).dialog("close");
                    remove();
                    new Williamson.Tour($('#appContainer')).init();
                },
                "No thanks": function() {
                    $(this).dialog("close");
                    remove();
                    new Williamson.Runs($('#appContainer')).init();

                }
            }
        });
    });
};

Williamson.Runs = function(element) {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="element">The element to render to</param>
    this.$runsEl = $('<span class="span12" data-bind="template: { name: \'runs-template\' }"></span>');
    $(element).append(this.$runsEl);
};

Williamson.Runs.prototype = {
    init: function() {

        //fetch runs data
        $.get("/api/Run", $.proxy(function(result) {

            ko.applyBindings({ runs: result }, this.$runsEl.get(0));
        }, this));
    }
};


Williamson.Tour = function(element) {
    /// <summary>
    /// Start the Tour component
    /// </summary>
    /// <param name="element">The element to put the tour in</param>
    this.$stepEl = $('<span data-bind="template: { name: \'step-template\' }"></span>');
    $(element).append(this.$stepEl);

};
Williamson.Tour.prototype = {
    init: function() {
        /// <summary>
        /// Starts the tour
        /// </summary>
        this._apply(
            "Using Web API Self Host",
            "This application is hosted using the ASP.NET SelfHost with a few hacks. Don\'t rely on it",
            function() {
                //do nothing
            },
            $.proxy(function() {
                this.step2();
            }, this),
            true,
            false
        );
    },
    step2: function() {
        /// <summary>
        /// Performs step 2
        /// </summary>
        this._apply(
            "Technologies",
            "I'm using a variety of technologies",
            $.proxy(function() {
                this.init();
            }, this),
            $.proxy(function() {
                this.init();
            }, this),
            false,
            true
        );
    },
    _apply: function(title, text, previousClick, nextClick, previousDisabled, nextDisabled) {
        /// <summary>
        /// Applies a step changes
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="text">The Text</param>
        /// <param name="previousClick">Event fired when the Previous button is clicked</param>
        /// <param name="nextClick">Event fired when the Next button is clicked</param>
        /// <param name="previousDisabled">Is the Previous button disabled</param>
        /// <param name="nextDisabled">Is the Next button disabled</param>
        if (previousDisabled == undefined) previousDisabled = false;
        if (nextDisabled == undefined) nextDisabled = false;
        ko.applyBindings({
            title: title,
            text: text
        }, this.$stepEl.get(0));

        this.$stepEl.find('#nextBtn').button({ disabled: nextDisabled }).click(nextClick);
        this.$stepEl.find('#previousBtn').button({ disabled: previousDisabled }).click(previousClick);
    }
};
