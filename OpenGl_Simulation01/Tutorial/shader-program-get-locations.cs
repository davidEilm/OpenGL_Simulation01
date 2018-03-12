public int GetAttributeLocation(string name)
{
    // get the location of a vertex attribute
    return GL.GetAttribLocation(this.handle, name);
}

public int GetUniformLocation(string name)
{
    // get the location of a uniform variable
    return GL.GetUniformLocation(this.handle, name);
}