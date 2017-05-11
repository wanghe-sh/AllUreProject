// JavaScript Document


//banner
$(document).ready(function(){
    var lis=$('.banner-arrows a');
    var imgs=$('.banner-list li');
	var oPrev=$('.banner-prev');
	var oNext=$('.banner-next');

    var iNow=0;
	
    lis.each(function(index){	
        $(this).mouseover(function(){
            slide(index);
        })
    });
	
    function slide(index){	
        iNow=index;
        lis.eq(index).addClass('cur').siblings().removeClass();	
        imgs.eq(index).fadeIn().siblings().fadeOut();
    }
	
	function autoRun(){	
        iNow++;
		if(iNow==lis.length){
			iNow=0;
		}
		slide(iNow);
	}
	
	var timer=setInterval(autoRun,3300);
		
    lis.hover(function(){
		clearInterval(timer);
	},function(){
		timer=setInterval(autoRun,3300);
    });
	
	oPrev.click(function(){
		clearInterval(timer);
		iNow--;
		slide(iNow);
		timer=setInterval(autoRun, 3300);	
		return false;
	});
	oNext.click(function(){
		clearInterval(timer);
		autoRun();
		timer=setInterval(autoRun, 3300);		
		return false;
	});
})


