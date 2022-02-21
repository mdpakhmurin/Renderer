#version 430 core

uniform mat4 cameraView;

uniform vec2 cameraSize;
uniform vec2 cameraPlaneDist;
uniform bool cameraIsPersp;

uniform ivec3 voxelGridSize;
layout(std430, binding = 3) buffer voxelGrid
{
    vec3 data_SSBO[];
};

in vec2 pos;

out vec4 outputColor;


float nearestDistanceToBox(vec3 point){
	vec3 boxSize = vec3(1);
	
	vec3 d = abs(point) + 0.5 - boxSize;

    return min(max(d.x, max(d.y, d.z)), 0) + length(max(d, 0));
}

vec4 nearestDistanceToBoxes(vec3 point){
	vec4 data = vec4(-1, -1, -1, 999999);
	for (int x = 0; x < voxelGridSize.x; x++){
		for (int y = 0; y < voxelGridSize.y; y++){
			for (int z = 0; z < voxelGridSize.z; z++){
				float dist = nearestDistanceToBox(point + vec3(x,y,z));
				if (dist < data[3])
					data = vec4(x,y,z,dist);
			}
		}
	}
	return data;
}

vec4 rayMarching(vec3 startPoint, vec3 direction){
	float dist = 0;
	vec4 data;

	for (int i = 0; i < 30; i++){
		data = nearestDistanceToBoxes(startPoint + dist*direction);
		
		if (data[3] < 0.02f){
			return vec4(data.xyz, dist);
		}

		dist += data[3];
	}

	return vec4(-1,-1,-1, 0);
}

void main()
{
	vec3 startPoint = vec3(pos, 0);
	
	vec3 endPoint;
	if (cameraIsPersp)
		endPoint = startPoint + normalize(vec3(pos, -cameraPlaneDist[0]));
	else
		endPoint = startPoint + vec3(0, 0, -1);

	startPoint = (cameraView * vec4(startPoint, 1)).xyz;
	endPoint = (cameraView * vec4(endPoint, 1)).xyz;

	vec4 nearDist = rayMarching(startPoint, normalize(endPoint - startPoint));
	
	if (nearDist[3] > 0){
		outputColor = vec4(data_SSBO[int(nearDist.x) + int(nearDist.y) * int(voxelGridSize.x) + int(nearDist.z) * int(voxelGridSize.y) * int(voxelGridSize.x)], 1);
	}
	else{
		float col = pos.y*10;
		if (col > 0.85)
			col = 0.85;
		if (col < 0.5)
			col = 0.5;

		outputColor = vec4(col, col, col, 1);
	}
}