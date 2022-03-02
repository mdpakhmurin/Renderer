#version 430 core

uniform mat4 cameraView;

uniform vec2 cameraSize;
uniform vec2 cameraPlaneDist;
uniform bool cameraIsPersp;

uniform ivec3 voxelGridSize;
layout(std430) buffer voxelGrid
{
    vec3 data_SSBO [];
};

in vec2 screenPoint;

out vec4 outputColor;

///////////////////////////////////////////

float nearestDistanceToBox(vec3 point)
{
    vec3 d = abs(point) - voxelGridSize;
    return min(max(d.x, max(d.y, d.z)), 0) + length(max(d, 0));
}

float rayMarching(vec3 startPoint, vec3 direction)
{
    float dist = 0;
    float nDist = 0;

    for (int i = 0; i < 100; i++)
    {
        nDist = nearestDistanceToBox(startPoint + dist * direction);

        if (nDist < 0.005f)
        {
            return dist;
        }

        dist += nDist;
    }

    return -1;
}

void main()
{
    vec3 startPoint = vec3(screenPoint, 0);

    vec3 endPoint;
    if (cameraIsPersp)
        endPoint = startPoint + normalize(vec3(screenPoint, cameraPlaneDist[0]));
    else
        endPoint = startPoint + vec3(0, 0, 1);

    startPoint = (cameraView * vec4(startPoint, 1)).xyz;
    endPoint = (cameraView * vec4(endPoint, 1)).xyz;
    vec3 dir = normalize(endPoint - startPoint);

    float dist = rayMarching(startPoint, dir);
    vec3 point = (startPoint + dir * dist);
    if (dist > 0){
        point.xyz += voxelGridSize;
        point.z += 1;
        point.xyz /= 2.0;
        point = min(point, voxelGridSize-1);

        int id = int(point.x) + int(point.y) * int(voxelGridSize.x) + int(point.z) * int(voxelGridSize.y) * int(voxelGridSize.x);
        outputColor = vec4(data_SSBO[id], 1);
    }
    else{
        discard;
    }
}