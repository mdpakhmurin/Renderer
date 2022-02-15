#version 430 core

uniform mat4 camera;
uniform ivec3 voxelGridSize;

layout(std430, binding = 3) buffer grid
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
		
		if (data[3] < 0.1f){
			return vec4(data.xyz, dist);
		}

		dist += data[3];
	}

	return vec4(-1,-1,-1, 0);
}

void main()
{
	vec2 newPos = pos / 0.9;

	vec3 startPoint = vec3(newPos*10, 0);
	vec3 endPoint = startPoint + vec3(0, 0, -1);

	startPoint = (camera * vec4(startPoint, 1)).xyz;
	endPoint = (camera * vec4(endPoint, 1)).xyz;

	vec4 nearDist = rayMarching(startPoint, endPoint - startPoint);
	
	if (nearDist[3] > 0){
		outputColor = vec4(data_SSBO[int(nearDist.x) + int(nearDist.y) * int(voxelGridSize.x) + int(nearDist.z) * int(voxelGridSize.y) * int(voxelGridSize.x)], 1);
	}
}