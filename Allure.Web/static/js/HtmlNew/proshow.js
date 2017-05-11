// JavaScript Document
$.extend({
	yulanscr:function(setting){
		var canshu=$.extend({
			gaoliang:'',                     //小图的高亮类
			kuandu:524,                      //显示区域宽度
			sudu:1000,                       //滚动速度
			xianshi:4,                       //显示个数
			bigimg:'',                       //大图的ul
			scrbar:'',                       //小图的ul
			left:'',                         //上一页
			right:''                         //下一页
		},setting);
		var simg=$(canshu.scrbar).children();               //获取小图对象
		var bigimg=$(canshu.bigimg);             //获取大图对象  
		var count=Math.ceil(simg.length/canshu.xianshi)-1;
		count=(count<0)?0:count;                            //计算可滚动次数
		var i=count;                                        //定义滚动计数器
		var next=0;                                         //定义图片的索引位置
		
		play();                                             //初始化
		
		//上一页，默认上一页的第一个显示，没有上一页则默认显示当前页的第一个
		$(canshu.left).click(function(){
			i++;
			if(i>count){
				i=count;
			}
			next=(count-i)*canshu.xianshi;
			play();
		})
		
		//下一页，默认下一页的第一个显示，没有下一页则默认显示当前页的第一个
		$(canshu.right).click(function(){
			i--;
			if(i<0){
				i=0;
			}
			next=(count-i)*canshu.xianshi;
			play();
		})
		
		//显示点击的图片
		simg.click(function(){
			next=$(this).index();
			play();
		})
		
		//点击大图预览下一张
		bigimg.click(function(){
			next=simg.filter("."+canshu.gaoliang).index()+1;
			next=(next>=simg.length)?0:next;
			play();
		})
		
		//显示程序
		function play(){
			var m=Math.ceil((next+1)/canshu.xianshi)-1;
			
			if(m<0){m=0;} //判断上下页滚动倍数
			$(canshu.scrbar).animate({marginTop:-m*canshu.kuandu},canshu.sudu);
			simg.eq(next).addClass(canshu.gaoliang).siblings().removeClass(canshu.gaoliang);
			bigimg.attr("src",simg.eq(next).find("img").attr("src"));
		}
	}	
	
})

