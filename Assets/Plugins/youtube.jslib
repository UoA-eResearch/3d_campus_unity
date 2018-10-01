mergeInto(LibraryManager.library, {
  ShowYoutube: function (str) {
    var videoId = Pointer_stringify(str);
    var iframe = document.createElement('iframe');
    iframe.src = "https://www.youtube.com/embed/" + videoId + "?rel=0";
    iframe.style.position = "absolute";
    iframe.style.top = "10%";
    iframe.style.left = "10%";
    iframe.style.width = "80%";
    iframe.style.height = "80%";
    iframe.setAttribute("frameborder", 0);
    iframe.setAttribute("allowFullScreen", 1);
    document.body.appendChild(iframe);
    document.body.onclick = function() { document.body.removeChild(iframe) }
  },
});
