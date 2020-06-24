<?php
/**
 * The template used for displaying featured posts on the Front Page.
 *
 * @package Monograph
 */
?>

<?php
	
	$featured_tag = get_theme_mod( 'monograph_featured_term_1', 'featured' );
	
	$custom_loop = new WP_Query( array(
		'post_type'      => 'post',
		'posts_per_page' => 3,
		'order'          => 'DESC',
		'orderby'        => 'date',
		'tag'	 	 	 => esc_html($featured_tag)
	) );
?>

<?php if ( $custom_loop->have_posts() ) : $i = 0; ?>

	<div class="ilovewp-featured-posts">
		
		<ul class="ilovewp-posts clearfix">

		<?php while ( $custom_loop->have_posts() ) : $custom_loop->the_post(); $i++; ?>

			<?php $classes = array('ilovewp-post','ilovewp-featured-post','featured-post-simple','featured-post-simple-'.$i); ?>
			<li <?php post_class($classes); ?>>
				<div class="ilovewp-post-wrapper">
					<?php if ( has_post_thumbnail() ) : ?>
					<div class="post-cover">
						<a href="<?php the_permalink(); ?>" title="<?php the_title_attribute(); ?>"><?php the_post_thumbnail('monograph-tall-thumbnail'); ?></a>
					</div><!-- .post-cover -->
					<?php endif; ?>
					<div class="post-preview">
						<div class="post-preview-wrapper">
							<span class="post-meta-category"><?php the_category(esc_html_x( ', ', 'Used on archive and post pages to separate multiple categories.', 'monograph' )); ?></span>
							<?php the_title( sprintf( '<h2 class="title-post"><a href="%s" rel="bookmark">', esc_url( get_permalink() ) ), '</a></h2>' ); ?>
						</div><!-- .post-preview-wrapper -->
					</div><!-- .post-preview -->
				</div><!-- .ilovewp-post-wrapper -->
			</li><!-- .ilovewp-post .ilovewp-featured-post .featured-post-simple .featured-post-simple-<?php echo $i; ?> -->
		
		<?php endwhile; ?>
		
		<?php wp_reset_postdata(); ?>

		</ul><!-- .ilovewp-posts .clearfix -->
	</div><!-- .ilovewp-featured-posts -->

<?php else : ?>

	<?php if ( current_user_can( 'publish_posts' ) && is_customize_preview() ) : ?>

		<div id="ilovewp-featured-posts">

			<div class="ilovewp-page-intro">
				<h1 class="title-page"><?php _e( 'No Featured Posts Found', 'monograph' ); ?></h1>
				<div class="taxonomy-description">

					<p><?php echo esc_html__( 'This section will display your featured posts. Configure (or disable) it via the Customizer.', 'monograph' ); ?><br>
					<?php /* translators: new post URL */ printf( wp_kses( __( 'You can mark your posts with a "Featured" tag on the Edit Post page. <a href="%1$s">Get started here</a>.', 'monograph' ), array( 'a' => array( 'href' => array() ) ) ), esc_url( admin_url( 'edit.php' ) ) ); ?></p>
					<p><strong><?php echo esc_html__( 'Important: This message is NOT visible to site visitors, only to admins and editors.', 'monograph' ); ?></strong></p>

				</div><!-- .taxonomy-description -->
			</div><!-- .ilovewp-page-intro -->

		</div><!-- #ilovewp-featured-posts -->

	<?php endif; ?>

<?php endif; ?>