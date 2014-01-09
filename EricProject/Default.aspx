<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
<script type="text/Javascript" >
    function Downlaod() {
        var cs = new CanvasSaver('http://localhost:2048/DownloadImage/DownloadImage.aspx')
        cs.generateButton('save an image!', cnvs, 'big.png');
        
    }

    function CanvasSaver(url) {

        this.url = url;

        this.savePNG = function (cnvs, fname) {
            if (!cnvs || !url) return;
            fname = fname || 'picture';

            var data = cnvs.toDataURL("image/png");
            data = data.substr(data.indexOf(',') + 1).toString();

            var dataInput = document.createElement("input");
            dataInput.setAttribute("name", 'imgdata');
            dataInput.setAttribute("value", data);
            dataInput.setAttribute("type", "hidden");

            var nameInput = document.createElement("input");
            nameInput.setAttribute("name", 'name');
            nameInput.setAttribute("value", fname + '.png');

            var myForm = document.createElement("form");
            myForm.method = 'post';
            myForm.action = url;
            myForm.appendChild(dataInput);
            myForm.appendChild(nameInput);

            document.body.appendChild(myForm);
            myForm.submit();
            document.body.removeChild(myForm);
        };

        this.generateButton = function (label, cnvs, fname) {
            var btn = document.createElement('button'), scope = this;
            btn.innerHTML = label;
            btn.style['class'] = 'canvassaver';
            btn.addEventListener('click', function () { scope.savePNG(cnvs, fname); }, false);
            scope.savePNG(cnvs, fname);
            

            return btn;
        };
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <canvas id="cnvs">
    <img src="DownloadImages/big.png" />
    </canvas>
       
<a href="#" id="ID_LINK" onclick="Downlaod();">Save Image</a>
    </div>
    </form>
    
</body>
</html>
