jQuery(document).ready(function($) {

	/**
	 *	Process request to dismiss our admin notice
	 */
	$('#monograph-admin-notice-postsnum .notice-dismiss').click(function() {

		//* Data to make available via the $_POST variable
		data = {
			action: 'monograph_admin_notice_postsnum',
			monograph_admin_notice_nonce: monograph_admin_notice_postsnum.monograph_admin_notice_nonce
		};

		//* Process the AJAX POST request
		$.post( ajaxurl, data );

		return false;
	});
});