/**
 * A code to be executed in MS Help Viewer 2.0 (not 1.x). 
 * MSHV 1.x:
 * - all <script> tags are removed
 * - <body onload="...  is untouched, so this is the only way to execute javascript
 * - HTML comments are persisted, so they can store any info, e.g. javascript
 * 
 * MSHV 2.0:
 * - all <script> tags inside <body> are removed, <script> tags inside <head> are untouched, as long as their
 *   contents is well formed XML (it's good to add <!-- or CDATA or even better place everything in external JS file).
 * - <body onload="...  is rewritten, so to do some tasks on load, we need to register onload event.
 * - HTML comments are removed
 */   

function mshv2_init() {
	try {
	// add init code here
	fixLinks();
	
	} catch (ex) {}
}		


// ***** LINK FIXING ******

// Escapes id query variables in internal links and corrects external links. 
function fixLinks() {
	try {
		var i;
		var links = document.documentElement.getElementsByTagName("a");
		for (i=0; i<links.length; i++) {
			var hrf = links[i].getAttribute("hr" + "ef");
			var escaped = escapeIdQueryVariable(hrf);
			
			var idValue = escaped[1];
			if (idValue != "") {
				var isExternal = (idValue.toLowerCase().indexOf("http://", 0) >= 0);
				isExternal = isExternal || (idValue.toLowerCase().indexOf("https://", 0) >= 0);
				var isHttp = isExternal;
				isExternal = isExternal || (idValue.toLowerCase().indexOf("mailto:", 0) >= 0);
				if (isExternal) {
					// external link
					//  Note: Help3 system 2.0+ converts each Id to lowercase.
					hrf = idValue;
					if (isHttp) {
						links[i].className = "mtps-external-link";
					}
				} else {
					// help3 link
					hrf = escaped[0];
					// # needs to be escaped twice
					hrf = hrf.replace("%23ctor", "%2523ctor");
				}
	
				links[i].setAttribute("hr" + "ef", hrf);
			}
		}
	} catch (ex) {
	}
}


var idQueryVariablePattern = /(\&|\?|^)(id\=)(.*?)(\&|$)/i;

// Returns array with 2 items. The first one is resulting complete queryString with
// escaped id variable. The second one is original value of id variable. 
function escapeIdQueryVariable(input) {
	var match = input.match(idQueryVariablePattern);
	var idValue = "";
	if (match) {
		idValue = input.match(idQueryVariablePattern)[3];
		input = input.replace(idQueryVariablePattern, "$1$2" + encodeURIComponent(idValue) + "$4");
	}
	var res = new Array();
	res[0] = input;
	res[1] = idValue;
	return res;
} 

// ***** END LINK FIXING ******


// *** CLASS DIAGRAM CLICKING ***
// To navigate any kind of link, we need to call click() method on the element.
// It's because of <MSHelp:link links in Help2 format (only used in IE). 
// Default click event in FF doesn't trigger the link (in IE it does).
// Fix it. 
function performClick(element) {
	if (document.createEventObject){
		// IE
		return element.click();
	}
	else{
		// dispatch for firefox + others
		var evt = element.ownerDocument.createEvent('MouseEvents');
		evt.initMouseEvent('click', true, true, element.ownerDocument.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
		element.dispatchEvent(evt);
	}
	
	while (element) {
		if (element.tagName == "A" && element.href != "") {
			if (element.target != "") {
				window.open(element.href, element.target);
			} else {
				document.location = element.href;
			}
			element = null;
		}	else {
			element = element.parentElement;
		}
	}
}

function clickElement(callerElement) {
	try {
		var elementHTML;
		var tagName;
		var innerCode;
		var attributes;
		var clickActionGuid;
		
		// get HTML source code of element to be clicked 
		clickActionGuid = callerElement.getAttribute("click-cref-guid");
		elementHTML = document.getElementById(clickActionGuid).innerHTML;
		
		// parse HTML element code
		var elementPattern = /<(.+?)\s+(.*)>(.*)<.+?>/igm;
		elementPattern.lastIndex = 0;
		var match = elementPattern.exec(elementHTML);
		tagName = match[1];
		attributes = match[2];
		innerCode = match[3];
		
		//get array of "attr=value" strings 	
		attributes = unescapeHTML(attributes);
		attributes = attributes.match(/\s*(.*?)\s*=\s*(["'])(.*?)\2/igm);
		
		// create element with attributes
		var el = document.createElement(tagName);
		//el.innerHTML = innerCode;
		var i;
		var attrPattern = /\s*(.*?)\s*=\s*(["'])(.*?)\2/igm;
		for (i=0; i<attributes.length; i++) {
			attrPattern.lastIndex = 0;
			match = attrPattern.exec(attributes[i]);
			el.setAttribute(match[1], match[3]);
		}

		document.body.appendChild(el);
		performClick(el);
	} catch (ex) {
	}
	return false;
}

function unescapeHTML (str) {
	return str.replace(/&amp;/g,"&").
		replace(/>/g,"&gt;").
		replace(/</g,"&lt;");//. 
		//the following doesn't work for some reason: replace(/"/g,"&quot;");                                         
}

// *** END CLASS DIAGRAM CLICKING ***


// Event handler attachment
function registerEventHandler (element, event, handler) {
	if (element.attachEvent) {
		element.attachEvent('on' + event, handler);
	} else if (element.addEventListener) {
		element.addEventListener(event, handler, false);
	} else {
		element[event] = handler;
	}
}

registerEventHandler(window, 'load', mshv2_init);
