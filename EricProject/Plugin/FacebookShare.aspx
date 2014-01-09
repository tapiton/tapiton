<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacebookShare.aspx.cs"
    Inherits="Plugin_FacebookShare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Facebook Share</title>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/FBShare.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //Validation start
        function CheckValidation() {
            if (document.getElementById('<%=txtComment.ClientID %>').value == "") {
                alert("Please write something...");
                return false;
            }
        }
        //Clear
        function Clear() {
            document.getElementById('<%=txtComment.ClientID %>').value = "";
            self.close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="pageheader">
        <div class="clearfix" id="header_container">
            <div class="lfloat">
                <h2 id="homelink">
                    Share This Link</h2>
            </div>
        </div>
    </div>
    <div class="pam" id="sharerDialog">
        <div class="-cx-PRIVATE-fbSharerFrame__stageArea">
            <div class="uiMentionsInput mbs -cx-PRIVATE-fbComposerXMentionsInput__root" id="u_0_4">
                <div class="highlighter">
                    <div>
                        <span class="highlighterContent"></span>
                    </div>
                </div>
                <div class="uiTypeahead -cx-PRIVATE-fbSharerFrame__typeaheadArea mentionsTypeahead"
                    id="u_0_5">
                    <div class="wrap">
                        <input type="hidden" autocomplete="off" class="hiddenInput"><div class="innerWrap">
                            <textarea class="DOMControl_placeholder uiTextareaNoResize uiTextareaAutogrow input mentionsTextarea textInput"
                                id="txtComment" runat="server" ></textarea></div>
                    </div>
                </div>
                <input type="hidden" autocomplete="off" class="mentionsHidden"></div>
            <div class="mtm -cx-PRIVATE-fbSharerFrame__shareStage">
                <div class="pam">
                    <div id="stage510c9773aed952387330571" class="UIShareStage clearfix UIShareStage_HasImage">
                        <div class="UIShareStage_Image">
                            <div class="UIShareStage_ThumbPager UIThumbPager" id="c510c9773af1911938129469">
                                <div class="UIThumbPager_Loader" style="display: none;">
                                    <img class="img" src="./Facebook_files/GsNJNwuI-UM.gif" alt="" width="16" height="11"></div>
                                <div class="UIThumbPager_Thumbs" id="imgDiv" runat="server">
                                    <img class="img" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/MerchantImage/<%=Campaign_Image%>" alt="" style="width: 100px;"></div>
                            </div>
                        </div>
                        <div class="UIShareStage_ShareContent">
                            <div class="UIShareStage_Title">
                                <span><a onclick="new InlineEditor(this, &quot;attachment[params][title]&quot;, $(&quot;stage510c9773aed952387330571&quot;), null, false, 100); return false;"
                                    class="UIShareStage_InlineEdit inline_edit"><%=Campaign_Title%></a></span></div>
                            <div class="UIShareStage_Subtitle">
                               <%=strShortURL%></div>
                                <div class="UIShareStage_Subtitle">
                                <br />
                              <%=DefaultFaceBook_Title%></div>
                            <div class="UIShareStage_Summary">
                                <p class="UIShareStage_BottomMargin">
                                    <a onclick="new InlineEditor(this, &quot;attachment[params][summary]&quot;, $(&quot;stage510c9773aed952387330571&quot;), null, true, 0); return false;"
                                        class="UIShareStage_InlineEdit inline_edit">&nbsp;&nbsp;&nbsp;</a></p>
                                <div class="UIShareStage_ThumbPagerControl UIThumbPagerControl UIThumbPagerControl_First UIThumbPagerControl_Last"
                                    id="c510c9773af7db1e44008511">
                                    <div class="UIThumbPagerControl_Buttons">
                                        <a class="UIThumbPagerControl_Button UIThumbPagerControl_Button_Left"></a><a class="UIThumbPagerControl_Button UIThumbPagerControl_Button_Right">
                                        </a>
                                    </div>
                                    <%--<div class="uiInputLabel clearfix mts">
                                        <input class="UIThumbPagerControl_NoPicture uiInputLabelCheckbox" type="checkbox"
                                            value="true" name="no_picture" id="u_0_3"><label for="u_0_3">No Thumbnail</label></div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="pam sharerButtonContainer clearfix uiBoxGray topborder">
        <div id="sharerDialogButtons" class="rfloat">
            <label class="uiButton uiButtonConfirm uiButtonLarge" for="u_0_0">
                <%--  <input value="Share Link" name="share" type="submit" id="u_0_0">--%>
                <asp:Button ID="btnShareLink" runat="server" Text="Share Link" OnClick="btnShareLink_Click"
                    OnClientClick="return CheckValidation();" />
            </label>
            <label class="uiButton uiButtonLarge" id="u_0_1" for="u_0_2">
                <input value="Cancel" type="submit" id="u_0_2" onclick="return Clear();">
            </label>
        </div>
    </div>
    </form>
</body>
</html>
