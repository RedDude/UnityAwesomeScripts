/* ====================================================
 * Company: Unity Technologies
 * Author:  Rickard Andersson, rickard@unity3d.com
 * 
   TABLE OF CONTENTS
   1. Sticky mobile header
   2. Mobile menu icons
   3. Footer links
   x. Global functions
   x. Mobile plugins
 *
======================================================= */

var menuOpen = false;

$(document).on('ready',function(){

/****************************************
  ==== 1. STICKY MOBILE HEADER
****************************************/

  if(masterW < 981){
    $('header.m-header').headroom();
  }

/****************************************
  ==== 2. MOBILE MENU ICONS
****************************************/

  // Toggle mobile menu
  $('.m-navbtn').on('click',function(){
    if(menuOpen){
      $(this).removeClass('close');
      $('html').removeClass('menuopen');
      $('.mobile-menu .wrap').removeAttr('style');
      $('.curtains').show();
      menuOpen = false;
    }
    else {
      $(this).addClass('close');
      $('.mobile-menu .wrap').height(getHeight());
      $('html').addClass('menuopen');
      $('.curtains').hide();
      menuOpen = true;
      prettifySearch();
    }
  });

  // Open menu and foucs search field
  $('.m-searchbtn').on('click',function(){
    if(menuOpen){
      $('.mobile-search input[type=text]').focus();
    }
    else {
      $('html').addClass('menuopen');
      $('.m-navbtn').addClass('close');
      menuOpen = true;
      prettifySearch(true);
    }
  });

  // Open menu and foucs search field
  $('.m-userbtn').on('click',function(){
    if(!menuOpen){
      $('.m-navbtn').trigger('click');
    }
  });

/****************************************
  ==== 3. FOOTER LINKS
****************************************/

  $('footer .links h3, footer .partners h3').on('click',function(){
    
    var $parent = $(this).parent();
    ($parent.hasClass('expanded')) ? $parent.removeClass('expanded') : $parent.addClass('expanded');
    renderFooter();
    
  });

});

/****************************************
  ==== XX. GLOBAL FUNCTIONS
****************************************/

function prettifySearch(focus){
  setTimeout(function(){
    if(focus){
      $('.mobile-search').contents().find('input.gsc-input').focus();
    }
    $('.mobile-search').contents().find('input.gsc-input').attr('placeholder','...');
    $('.mobile-search').contents().find('input.gsc-search-button-v2').attr('src',blank).addClass('g-search-icon');
  },150);
}


/****************************************
  ==== XX. MOBILE PLUGINS
****************************************/

/*!
 * headroom.js v0.6.0 - Give your page some headroom. Hide your header until you need it
 * Copyright (c) 2014 Nick Williams - http://wicky.nillia.ms/headroom.js
 * License: MIT
 */

!function(a,b){"use strict";function c(a){this.callback=a,this.ticking=!1}function d(a){if(arguments.length<=0)throw new Error("Missing arguments in extend function");var b,c,e=a||{};for(c=1;c<arguments.length;c++){var f=arguments[c]||{};for(b in f)e[b]="object"==typeof e[b]?d(e[b],f[b]):e[b]||f[b]}return e}function e(a){return a===Object(a)?a:{down:a,up:a}}function f(a,b){b=d(b,f.options),this.lastKnownScrollY=0,this.elem=a,this.debouncer=new c(this.update.bind(this)),this.tolerance=e(b.tolerance),this.classes=b.classes,this.offset=b.offset,this.initialised=!1,this.onPin=b.onPin,this.onUnpin=b.onUnpin,this.onTop=b.onTop,this.onNotTop=b.onNotTop}var g={bind:!!function(){}.bind,classList:"classList"in b.documentElement,rAF:!!(a.requestAnimationFrame||a.webkitRequestAnimationFrame||a.mozRequestAnimationFrame)};a.requestAnimationFrame=a.requestAnimationFrame||a.webkitRequestAnimationFrame||a.mozRequestAnimationFrame,c.prototype={constructor:c,update:function(){this.callback&&this.callback(),this.ticking=!1},requestTick:function(){this.ticking||(requestAnimationFrame(this.rafCallback||(this.rafCallback=this.update.bind(this))),this.ticking=!0)},handleEvent:function(){this.requestTick()}},f.prototype={constructor:f,init:function(){return f.cutsTheMustard?(this.elem.classList.add(this.classes.initial),setTimeout(this.attachEvent.bind(this),100),this):void 0},destroy:function(){var b=this.classes;this.initialised=!1,a.removeEventListener("scroll",this.debouncer,!1),this.elem.classList.remove(b.unpinned,b.pinned,b.top,b.initial)},attachEvent:function(){this.initialised||(this.lastKnownScrollY=this.getScrollY(),this.initialised=!0,a.addEventListener("scroll",this.debouncer,!1),this.debouncer.handleEvent())},unpin:function(){var a=this.elem.classList,b=this.classes;(a.contains(b.pinned)||!a.contains(b.unpinned))&&(a.add(b.unpinned),a.remove(b.pinned),this.onUnpin&&this.onUnpin.call(this))},pin:function(){var a=this.elem.classList,b=this.classes;a.contains(b.unpinned)&&(a.remove(b.unpinned),a.add(b.pinned),this.onPin&&this.onPin.call(this))},top:function(){var a=this.elem.classList,b=this.classes;a.contains(b.top)||(a.add(b.top),a.remove(b.notTop),this.onTop&&this.onTop.call(this))},notTop:function(){var a=this.elem.classList,b=this.classes;a.contains(b.notTop)||(a.add(b.notTop),a.remove(b.top),this.onNotTop&&this.onNotTop.call(this))},getScrollY:function(){return void 0!==a.pageYOffset?a.pageYOffset:(b.documentElement||b.body.parentNode||b.body).scrollTop},getViewportHeight:function(){return a.innerHeight||b.documentElement.clientHeight||b.body.clientHeight},getDocumentHeight:function(){var a=b.body,c=b.documentElement;return Math.max(a.scrollHeight,c.scrollHeight,a.offsetHeight,c.offsetHeight,a.clientHeight,c.clientHeight)},isOutOfBounds:function(a){var b=0>a,c=a+this.getViewportHeight()>this.getDocumentHeight();return b||c},toleranceExceeded:function(a,b){return Math.abs(a-this.lastKnownScrollY)>=this.tolerance[b]},shouldUnpin:function(a,b){var c=a>this.lastKnownScrollY,d=a>=this.offset;return c&&d&&b},shouldPin:function(a,b){var c=a<this.lastKnownScrollY,d=a<=this.offset;return c&&b||d},update:function(){var a=this.getScrollY(),b=a>this.lastKnownScrollY?"down":"up",c=this.toleranceExceeded(a,b);this.isOutOfBounds(a)||(a<=this.offset?this.top():this.notTop(),this.shouldUnpin(a,c)?this.unpin():this.shouldPin(a,c)&&this.pin(),this.lastKnownScrollY=a)}},f.options={tolerance:{up:0,down:0},offset:0,classes:{pinned:"headroom--pinned",unpinned:"headroom--unpinned",top:"headroom--top",notTop:"headroom--not-top",initial:"headroom"}},f.cutsTheMustard="undefined"!=typeof g&&g.rAF&&g.bind&&g.classList,a.Headroom=f}(window,document);

(function($) {

  if(!$) {
    return;
  }

  $.fn.headroom = function(option) {
    return this.each(function() {
      var $this   = $(this),
        data      = $this.data('headroom'),
        options   = typeof option === 'object' && option;

      options = $.extend(true, {}, Headroom.options, options);

      if (!data) {
        data = new Headroom(this, options);
        data.init();
        $this.data('headroom', data);
      }
      if (typeof option === 'string') {
        data[option]();
      }
    });
  };

  $('[data-headroom]').each(function() {
    var $this = $(this);
    $this.headroom($this.data());
  });

}(window.Zepto || window.jQuery));
