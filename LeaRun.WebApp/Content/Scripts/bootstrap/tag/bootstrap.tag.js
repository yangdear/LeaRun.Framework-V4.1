!function ($) {

  "use strict"; // jshint ;_;


 /* TAGS CLASS DEFINITION
  * ====================== */

  var dismiss = '[data-dismiss="tags"]'
    , Tags = function (element, options) {    
        this.$element = $(element).addClass('tags')
        this.options = $.extend({}, $.fn.tags.defaults, options)
        this.store = [];
        this.$element.on('click', dismiss, this.close)
      }

  Tags.prototype.add = function (e) {
    var data;
    if(typeof e == 'object'){
      data = {
        value: e.value,
        text: e.text
      };
    }else if(typeof e == 'string'){
      data = {
        value: e,
        text: e
      };
    }
    if( this.check(data) ){ 
        this.store.push(data);
        this.addTag(data)
    }
  }
  
  Tags.prototype.check = function (data) { 
    var flag, key = data.value;
    $.each(this.store, function(){
      if(this.value == key){
        flag = true;
        return false;
      }      
    });
    return !flag
  }
  
  Tags.prototype.remove = function (key) { 
    var store = this.store, index = -1;
    $.each(store, function(i, v){
      if(this.value == key){
        index = i; 
        return false;
      }      
    });
    
    if(index > -1){
      store.splice(index, 1);    
      this.removeTag(key)
    }
  }
  
  Tags.prototype.addTag = function (data) { 
    var opts = this.options        
      , $tag = $(opts.template)
      
    $tag.attr({
      'data-tag-value': data.value,
      'data-tag-text': data.text
    }).find('.tag-content').text(data.text)
    
    $tag.appendTo(this.$element)
    
    $.isFunction(opts.callbacks.add) && opts.callbacks.add(this, $tag);    
  }
  
  Tags.prototype.removeTag = function (key) { 
    var opts = this.options
      , $tag = this.$element.find('.tag[data-tag-value="' + key + '"]')
    
    $.isFunction(opts.callbacks.remove) && opts.callbacks.remove(this, $tag);
    
    $tag.remove();
  }
  
  Tags.prototype.serialize = function () {
    var texts = []
      , values = [];
    $.each(this.store, function(){
      texts.push(this.text);
      values.push(this.value);
    });
    return {
        value: values.join(','),
        text: texts.join(',')
    }
  }  
  
  Tags.prototype.clear = function () {
    var self = this
      , opts = this.options
    this.store = [];
    this.$element.find('.tag').remove();
    $.isFunction(opts.callbacks.clear) && opts.callbacks.clear(this);
  }
  
  Tags.prototype.close = function (e) {
    var $this = $(this)
      , $parent = $this.parent('.tag').first()
      , api = $this.parents('.tags').first().data('tags')

    e && e.preventDefault()
    
    api.remove( $parent.attr('data-tag-value') );
  
  }


 /* TAGS PLUGIN DEFINITION
  * ======================= */

  var old = $.fn.tags

  $.fn.tags = function (option) {
    return this.each(function () {
      var $this = $(this)
        , data = $this.data('tags')
      if (!data) $this.data('tags', (data = new Tags(this, option)))
      if (typeof option == 'string') data[option].call($this)
    })
  }

  $.fn.tags.Constructor = Tags

  $.fn.tags.defaults = {
    source: [],
    callbacks: {
        add: null,
        remove: null,
        clear: null
    },
    template: '<span class="tag"><a href="javascript:void(0)" class="close" data-dismiss="tags">¡Á</a><span class="tag-content"></span></span>'
  }

 /* TAGS NO CONFLICT
  * ================= */

  $.fn.tags.noConflict = function () {
    $.fn.tags = old
    return this
  }


}(window.jQuery);