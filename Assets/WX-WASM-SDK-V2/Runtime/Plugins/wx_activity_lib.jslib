mergeInto(LibraryManager.library, {
WX_GetPhoneNumber:function(activityId) {
    window.WXWASMSDK.WX_GetPhoneNumber(Pointer_stringify(activityId))
}
})