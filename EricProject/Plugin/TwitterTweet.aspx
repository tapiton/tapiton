<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TwitterTweet.aspx.cs" Inherits="Plugin_TwitterTweet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Twitter Tweet</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <script type="text/javascript">
        //Validation start
        function CheckValidation() {
            if (document.getElementById('<%=txtComment.ClientID %>').value == "") {
                alert("Please write something...");
                return false;
            }
        }

        function CountLeft(field, count, max) {
            // if the length of the string in the input field is greater than the max value, trim it
            if (field.value.length > max)
                field.value = field.value.substring(0, max);
            else
            // calculate the remaining characters
                count.value = max - field.value.length;
            document.getElementById('<%=hfComment.ClientID %>').value = field.value;
        }
    </script>
</head>
<body style="background:none;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfComment" runat="server" />
    <div class="socialLeft">
        <div class="socialSec">
            <div class="twitterbar">
                <div class="fl">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/twitter_logo.jpg"
                        alt="" /></div>
                <div class="fr">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/twitter_bird.jpg"
                        alt="" /></div>
                <div class="clr">
                </div>
            </div>
            <div id="tab_1">
                <div class="postHd">
                    Share a link with your friends or Family.</div>
                <div class="twitterround">
                    <div class="fl">
                        <textarea rows="5" cols="5" class="msg" id="txtComment" runat="server" ClientIDMode="Static"
                        onkeydown="CountLeft(this.form.txtComment,this.form.txtcount,115)"
                             onkeyup="CountLeft(this.form.txtComment,this.form.txtcount,115)"></textarea>
                        <div class="note">
                        <asp:TextBox ID="txtcount" name="txtcount" runat="server" ClientIDMode="Static" Enabled="false"  Width="30px" style="background-color:Red;color:White;border-width:0px;"></asp:TextBox>
                         <%--    <a href="#"><span>47</span></a>--%>
                        </div>
                    </div>
                    <div class="btn">
                        <asp:ImageButton ID="ibtn" runat="server" ImageUrl="../images/tweetbtn.png" OnClick="ibtn_Click" />
                    </div>
                    <div class="clr">
                    </div>
                    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
       
    </form>
</body>
</html>
