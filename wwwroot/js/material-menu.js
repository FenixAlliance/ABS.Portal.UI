document.addEventListener("touchstart", function() {},false);
$(function() {
	$('.material-navtoggle').click(function () {
		$('.material-menucontainer').toggleClass('wsoffcanvasopener');
	});
	
	$('.overlapblackbg').click(function () {
	  $('.material-menucontainer').removeClass('wsoffcanvasopener');
	});

	$('.material-menu-list> li').has('.material-menu-submenu').prepend('<span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>');
	$('.material-menu-list > li').has('.megamenu').prepend('<span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>');
	$('.material-menu-click').click(function(){
		$(this).toggleClass('ws-activearrow')
		.parent().siblings().children().removeClass('ws-activearrow');
		$(".material-menu-submenu, .megamenu").not($(this).siblings('.material-menu-submenu, .megamenu')).slideUp('slow');
		$(this).siblings('.material-menu-submenu').slideToggle('slow');
		$(this).siblings('.megamenu').slideToggle('slow');	
	});
	
	$('.material-menu-list > li > ul > li').has('.material-menu-submenu-sub').prepend('<span class="wsmenu-click02"><i class="wsmenu-arrow fa fa-angle-down"></i></span>');
	$('.material-menu-list > li > ul > li > ul > li').has('.material-menu-submenu-sub-sub').prepend('<span class="wsmenu-click02"><i class="wsmenu-arrow fa fa-angle-down"></i></span>');
	
	$('.material-menu-click02').click(function(){
		$(this).children('.material-menu-arrow').toggleClass('wsmenu-rotate');
		$(this).siblings('.material-menu-submenu-sub').slideToggle('slow');
		$(this).siblings('.material-menu-submenu-sub-sub').slideToggle('slow');
	});
	
});