namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGL2FramebufferType
    {
        /// <summary>Collection buffer data storage of color, alpha, depth and stencil buffers used as both a destination for drawing and as a source for reading.</summary>
        FRAMEBUFFER = 0x8D40,

        // WebGL2
        /// <summary>Used as a source for reading operations such as IWebGLRenderingContext.ReadPixels() and gl.BlitFramebuffer().</summary>
        READ_FRAMEBUFFER = 0x8CA8,
        /// <summary>Used as a destination for drawing operations such as IWebGLRenderingContext.Draw*(), IWebGLRenderingContext.Clear*() and BlitFramebuffer().</summary>
        DRAW_FRAMEBUFFER = 0x8CA9,
    }
}
