#version 430 core

uniform vec2 cameraSize;

layout(location = 0) in vec2 inPos;
out vec2 screenPoint;


void main(void)
{
    gl_Position = vec4(inPos, 0.0, 1.0);

    screenPoint = vec2(inPos.x * cameraSize.x * 0.5, inPos.y * cameraSize.y * 0.5);
}