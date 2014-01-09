function clearText(thefield) {
  if (thefield.defaultValue==thefield.value) { thefield.value = "" }
} 
function replaceText(thefield) {
  if (thefield.value=="") { thefield.value = thefield.defaultValue }
}

function showPanel(c,t)
	{
	    document.getElementById(c).style.display = 'block';
		//document.getElementById(t).style.border='2px solid #e4f0fa';
	}
function hidePanel(c, t) 
    {
        document.getElementById(c).style.display = 'none';
		//document.getElementById(t).style.border='2px solid #ffffff';
	}

function pro_show(tab) 
{
	i=1;
	
	while (document.getElementById("tab_"+i))
	 {
	 document.getElementById("tab_"+i).style.display='none';
	 i++;
	 
	 }
	 document.getElementById(tab).style.display='block';
}

function tab_show(tab,num) 
{
 i=1;
 
 while (document.getElementById(tab+'_'+i))
  {
      document.getElementById(tab + '_' + i).style.display = 'none';
  document.getElementById(tab+i).className='';
  i++;  
  }
  document.getElementById(tab+'_'+num).style.display='block';
  document.getElementById(tab+num).className='sel';
}

function fp_show(tabopen, cl) {
    i = 1;

    while (document.getElementById("a" + i)) {
        document.getElementById("a" + i).className = 'view-list-item';
        i++;

    }
    document.getElementById(cl).className = 'view-list-item sel';
}
function radio_show(tab) {
    i = 1;

    while (document.getElementById("tab_" + i)) {
        document.getElementById("tab_" + i).style.display = 'none';
        i++;

    }
    document.getElementById(tab).style.display = 'block';
}



function show_pop(e) {
    document.getElementById(e).style.display = 'block';

}

function hide_pop(e) {
    document.getElementById(e).style.display = 'none';

}
function pop_show(e) {
    document.getElementById(e).style.display = 'block';
}

function sort_data(d) {
    var v = d.innerHTML
    document.getElementById('sortby').innerHTML = v;
    document.getElementById('sort').style.display = 'none';
}

function sort_search(d) {
    var v = d.innerHTML
    document.getElementById('sorting').innerHTML = v;
    document.getElementById('sot').style.display = 'none';
}